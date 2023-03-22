using System.Collections.Generic;
using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Data.StaticData.Grid;
using Trampoline.CodeBase.Data.StaticData.Sounds;
using Trampoline.CodeBase.Services.StaticData.StaticDataProvider;

namespace Trampoline.CodeBase.Services.StaticData
{
    public class StaticData : IStaticData
    {
        private readonly IStaticDataProvider _staticDataProvider;
        public SoundData SoundData { get; private set; }
        public GameConfig GameConfig { get; private set; }
        public PrefabsData Prefabs { get; private set; }
        public Dictionary<ElementType, Element> ElementsByType { get; private set; }
        public GridConfig GridConfig { get; private set; }
        public Element[] Elements { get; private set; }
        public SpritesData SpritesData { get; private set; }


        public StaticData(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            LoadStaticData();
            CreateElementSprites();
        }

        private void CreateElementSprites()
        {
            ElementsByType = new();
            Elements = _staticDataProvider.LoadElementsData().Elements;
            foreach (Element element in Elements) 
                ElementsByType.Add(element.Type, element);
        }

        private void LoadStaticData()
        {
            SoundData = _staticDataProvider.LoadSoundData();
            GameConfig = _staticDataProvider.LoadGameConfig();
            Prefabs = _staticDataProvider.LoadPrefabsData();
            GridConfig = _staticDataProvider.LoadGridConfig();
            SpritesData = _staticDataProvider.LoadSprites();
        }
    }
}