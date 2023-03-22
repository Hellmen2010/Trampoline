using System;
using Trampoline.CodeBase.Data.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trampoline.CodeBase.Core.Element
{
    public class BallView : MonoBehaviour
    {
        public event Action<BallView> OnGroundTouched;
        public event Action<BallView> OnBallDelivered;
        
        public ElementType Type;
        
        [SerializeField] private SpriteRenderer _image;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private LayerMask _basket;
        [SerializeField] private LayerMask _light;

        public void Construct(Element element)
        {
            _image.sprite = element.Sprite;
            Type = element.Type;
        }

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        public void AddForce(float amount)
        {
            float rand = Random.Range(0.12f, 0.135f);
            _rb.AddForce(new Vector2(rand, 1) * amount, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (1 << collision.gameObject.layer != _ground) return;
            OnGroundTouched?.Invoke(this);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (1 << col.gameObject.layer == _basket)
            {
                OnBallDelivered?.Invoke(this);
            }
            else if(1 << col.gameObject.layer == _light)
            {
                //_rb.velocity = new Vector2(0, 0);
                _rb.AddForce(new Vector2(0.1f, -1f) *5, ForceMode2D.Impulse);
            }
        }

        public void UnSubscribe()
        {
            OnGroundTouched = null;
            OnBallDelivered = null;
        }

        public void SetSpawnPosition(Vector3 position) => transform.position = position;
    }
}