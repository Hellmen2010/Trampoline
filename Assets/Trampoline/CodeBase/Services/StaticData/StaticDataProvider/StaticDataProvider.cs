using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Data.StaticData.Grid;
using Trampoline.CodeBase.Data.StaticData.Sounds;
using UnityEngine;

namespace Trampoline.CodeBase.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string SoundDataPath = "StaticData/SoundData";
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string PrefabsDataPath = "StaticData/PrefabsData";
        private const string ElementDataPath = "StaticData/ElementData";
        private const string GridConfigPath = "StaticData/GridConfig";
        private const string SpritesDataPath = "StaticData/SpritesData";
        public SoundData LoadSoundData() => Resources.Load<SoundData>(SoundDataPath);
        public GameConfig LoadGameConfig() => Resources.Load<GameConfig>(GameConfigPath);
        public PrefabsData LoadPrefabsData() => Resources.Load<PrefabsData>(PrefabsDataPath);
        public ElementData LoadElementsData() => Resources.Load<ElementData>(ElementDataPath);
        public GridConfig LoadGridConfig() => Resources.Load<GridConfig>(GridConfigPath);
        public SpritesData LoadSprites() => Resources.Load<SpritesData>(SpritesDataPath);
    }
}