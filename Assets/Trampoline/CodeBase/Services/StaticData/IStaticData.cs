using System.Collections.Generic;
using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Data.StaticData.Grid;
using Trampoline.CodeBase.Data.StaticData.Sounds;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Trampoline.CodeBase.Services.StaticData
{
    public interface IStaticData : IService
    {
        SoundData SoundData { get; }
        GameConfig GameConfig { get; }
        PrefabsData Prefabs { get; }
        Dictionary<ElementType, Element> ElementsByType { get; }
        GridConfig GridConfig { get; }
        Element[] Elements { get; }
        SpritesData SpritesData { get; }
    }
}