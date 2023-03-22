using System;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}