using Trampoline.CodeBase.Infrastructure.States;
using Trampoline.CodeBase.Services.Sound;
using UnityEngine;

namespace Trampoline.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SoundService _soundService;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _soundService);
            _game.StateMachine.Enter<BootstrapperState>();

            DontDestroyOnLoad(this);
        }
    }
}