using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Services.StaticData;

namespace Trampoline.CodeBase.Services.PersistentProgress
{
    public class PersistentProgress : IPersistentProgress
    {
        private readonly IStaticData _staticData;
        public PlayerProgress Progress { get; set; }

        public PersistentProgress(IStaticData staticData)
        {
            _staticData = staticData;
        }
        
        public void AddStatisticElement(ElementType type)
        {
            //if more then cut?
            if (Progress.StatisticsElements.Count == _staticData.GridConfig.Columns * _staticData.GridConfig.Rows + 1)
            {
                Progress.StatisticsElements.RemoveAt(0);
                Progress.StatisticsElements.Add(type);
            }
            else
            {
                Progress.StatisticsElements.Add(type);
            }
        }
    }
}