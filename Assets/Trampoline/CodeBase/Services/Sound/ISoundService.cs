using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Data.StaticData.Sounds;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;

namespace Trampoline.CodeBase.Services.Sound
{
    public interface ISoundService : IService
    {
        void Construct(SoundData soundData, Settings progressSettings);
        void EnableBackgroundMusic();
        void DisableBackgroundMusic();
        void PlayEffectSound(SoundId soundId);
        void SetBackgroundVolume(float volume);
        void SetEffectsVolume(float volume);
    }
}