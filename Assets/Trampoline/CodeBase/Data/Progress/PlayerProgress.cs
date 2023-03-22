using System;
using System.Collections.Generic;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Services.StaticData;

namespace Trampoline.CodeBase.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        private readonly IStaticData _staticData;
        public Settings Settings;
        public List<ElementType> StatisticsElements;

        public PlayerProgress(IStaticData staticData)
        {
            _staticData = staticData;
            Settings = new Settings();
            StatisticsElements = new List<ElementType>();
        }
    }
}