using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgress : IService
    {
        PlayerProgress Progress { get; set; }
        void AddStatisticElement(ElementType type);
    }
}