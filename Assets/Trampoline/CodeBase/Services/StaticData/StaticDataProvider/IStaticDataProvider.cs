using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Data.StaticData.Grid;
using Trampoline.CodeBase.Data.StaticData.Sounds;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider : IService
    {
        SoundData LoadSoundData();
        GameConfig LoadGameConfig();
        PrefabsData LoadPrefabsData();
        ElementData LoadElementsData();
        GridConfig LoadGridConfig();
        SpritesData LoadSprites();
    }
}