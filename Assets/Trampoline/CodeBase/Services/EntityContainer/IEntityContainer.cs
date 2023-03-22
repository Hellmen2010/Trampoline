using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.EntityContainer
{
    public interface IEntityContainer : IService
    {
        void RegisterEntity<TEntity>(TEntity entity) where TEntity : class;
        TEntity GetEntity<TEntity>();
    }
}