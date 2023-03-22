using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Services.Factories.GameFactory;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Trampoline.CodeBase.Core.Spawner
{
    public class BallSpawner
    {
        public event Action<int> OnBallRelease;
        public event Action OnBallLose;
        public event Action<ElementType> OnBallDelivered;
        
        private readonly Element.Element[] _elements;
        private readonly BallSpawnerView _view;
        private ObjectPool<BallView> _ballsPool;
        private readonly GameConfig _config;
        private readonly Transform _addForceTransform;
        private readonly float _throwForceMin;
        private readonly float _throwForceMax;
        private Coroutine _spawnRoutine;
        private List<BallView> _activeBalls;
        private bool _roundActive;
        private float _yAddForcePoint;
        private float _ballMoveDownTime;

        public BallSpawner(BallSpawnerView view, IStaticData staticData)
        {
            _view = view;
            _config = staticData.GameConfig;
            _yAddForcePoint = _config.YAddForcePoint;
            _ballMoveDownTime = _config.BallMoveDownTime;
            _throwForceMin = _config.BallThrowForceMin;
            _throwForceMax = _config.BallThrowForceMax;
            _elements = staticData.Elements;
            _activeBalls = new List<BallView>();
        }

        public void CreateBallsPool(IGameFactory gameFactory)
        {
            _ballsPool = new ObjectPool<BallView>(
                () => gameFactory.CreateBallView(),
                ballView =>
                {
                    _activeBalls.Add(ballView);
                },
                ballView =>
                {
                    OnBallRelease?.Invoke(_ballsPool.CountActive);
                    ballView.UnSubscribe();
                    _activeBalls.Remove(ballView);
                    ballView.gameObject.SetActive(false);
                },
                ballView =>
                {

                    Object.Destroy(ballView.gameObject);
                },
                true, 10, 20);
        }

        public void StartSpawnBalls()
        {
            _roundActive = true;
            _spawnRoutine = _view.StartCoroutine(SpawnBallsRoutine());
        }

        public void StopSpawnRoutine() => _view.StopCoroutine(_spawnRoutine);

        public void UnSubscribe()
        {
            OnBallLose = null;
            OnBallDelivered = null;
            OnBallRelease = null;
        }

        private IEnumerator SpawnBallsRoutine()
        {
            while (_roundActive)
            {
                ThrowBall();
                yield return new WaitForSeconds(_config.BallsSpawnDelay);
            }
        }

        private void ThrowBall()
        {
            BallView ball = _ballsPool.Get();
            float randForce = Random.Range(_throwForceMin, _throwForceMax);
            ball.SetSpawnPosition(_view.transform.position);
            ball.Construct(_elements[Random.Range(0, _elements.Length)]);
            ball.Enable();
            ball.OnGroundTouched += HideBall;
            ball.OnGroundTouched += LoseBall;
            ball.OnBallDelivered += BallDelivered;
            MoveBall(ball, randForce);
        }

        private void MoveBall(BallView ball, float randForce)
        {
            ball.transform.DOMoveY(_yAddForcePoint, _ballMoveDownTime)
                .OnComplete(() => ball.AddForce(randForce));
        }

        private void LoseBall(BallView view) => OnBallLose?.Invoke();

        private async void BallDelivered(BallView ballView)
        {
            OnBallDelivered?.Invoke(ballView.Type);
            await Task.Delay(200);
            HideBall(ballView);
        }

        private void HideBall(BallView ballView)
        {
            _ballsPool.Release(ballView);
        }
    }
}