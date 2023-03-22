using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.SaveLoad
{
    public interface ISaveLoad : IService
    {
        PlayerProgress LoadProgress();
        void SaveProgress();
    }
}