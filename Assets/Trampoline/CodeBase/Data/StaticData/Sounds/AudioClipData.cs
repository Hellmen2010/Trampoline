using System;
using Trampoline.CodeBase.Data.Enums;
using UnityEngine;

namespace Trampoline.CodeBase.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }
}