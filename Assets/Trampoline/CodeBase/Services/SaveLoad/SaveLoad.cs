using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Extensions;
using Trampoline.CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace Trampoline.CodeBase.Services.SaveLoad
{
    public class SaveLoad : ISaveLoad
    {
        private readonly IPersistentProgress _playerProgress;
        private const string ProgressKey = "Progress";

        public SaveLoad(IPersistentProgress playerProgress) => _playerProgress = playerProgress;
        
        public void SaveProgress() => 
            PlayerPrefs.SetString(ProgressKey, _playerProgress.Progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    }
}