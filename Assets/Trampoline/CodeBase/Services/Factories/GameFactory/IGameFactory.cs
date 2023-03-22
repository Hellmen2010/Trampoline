using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.Factories.GameFactory
{
    public interface IGameFactory : IService
    {
        void CreateBallSpawner();
        BallView CreateBallView();
        void CreateTrampoline();
    }
}