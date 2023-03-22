using Trampoline.CodeBase.Infrastructure.ServiceContainer;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.SceneLoader;
using Trampoline.CodeBase.Services.Sound;

namespace Trampoline.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, coroutineRunner, soundService);
        }
    }
}