using System;
using System.Collections.Generic;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.GameFactory;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.PersistentProgress;
using Trampoline.CodeBase.Services.SaveLoad;
using Trampoline.CodeBase.Services.SceneLoader;
using Trampoline.CodeBase.Services.Sound;
using Trampoline.CodeBase.Services.StaticData;

namespace Trampoline.CodeBase.Infrastructure.States.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services,
            ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapperState)] = CreateBootstrapperState(sceneLoader, services, soundService, coroutineRunner),
                [typeof(MainMenuState)] = CreateMainMenuState(sceneLoader, services),
                [typeof(LoadProgressState)] = CreateLoadProgressState(services, soundService),
                [typeof(CreatePersistentEntitiesState)] = CreatePersistenEntitiesState(services),
                [typeof(CreateGameState)] = CreateCreateGameState(sceneLoader, services),
                [typeof(PreparationState)] = CreatePreparationState(services, coroutineRunner),
                [typeof(GameLoopState)] = CreateGameLoopState(services),
                [typeof(ResultState)] = new ResultState(this, services.Single<IEntityContainer>()),
            };
        }

        private PreparationState CreatePreparationState(AllServices services, ICoroutineRunner coroutineRunner) => 
            new (this, services.Single<IEntityContainer>(), coroutineRunner, services.Single<IStaticData>());

        private GameLoopState CreateGameLoopState(AllServices services) => 
            new (this, services.Single<IEntityContainer>(), services.Single<IPersistentProgress>(), services.Single<ISaveLoad>());

        private CreatePersistentEntitiesState CreatePersistenEntitiesState(AllServices services) => 
            new (this, services.Single<IUIFactory>(), services.Single<IEntityContainer>(), services.Single<IStaticData>());

        private BootstrapperState CreateBootstrapperState(SceneLoader sceneLoader, AllServices services,
            ISoundService soundService, ICoroutineRunner coroutineRunner) => 
            new (this, sceneLoader, services, soundService, coroutineRunner);

        private MainMenuState CreateMainMenuState(SceneLoader sceneLoader, AllServices services) =>
            new (sceneLoader, this, services.Single<IUIFactory>(),
                services.Single<IEntityContainer>(), services.Single<IPersistentProgress>());

        private CreateGameState CreateCreateGameState(SceneLoader sceneLoader, AllServices services) => 
            new (this, sceneLoader, services.Single<IUIFactory>(), services.Single<IGameFactory>(), services.Single<IEntityContainer>());

        private LoadProgressState CreateLoadProgressState(AllServices services, ISoundService soundService) =>
            new (this, services.Single<IPersistentProgress>(),
                services.Single<IStaticData>(), services.Single<ISaveLoad>(), soundService);


        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }
    }
}