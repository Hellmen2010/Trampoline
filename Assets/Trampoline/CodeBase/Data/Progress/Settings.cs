using System;

namespace Trampoline.CodeBase.Data.Progress
{
    [Serializable]
    public class Settings
    {
        public bool IsEffectsSoundActive;
        public bool IsMusicSoundActive;
        public float EffectsVolume;
        public float MusicVolume;

        public Settings()
        {
            IsEffectsSoundActive = IsMusicSoundActive = true;
            EffectsVolume = MusicVolume = 0.55f;
        }
    }
}