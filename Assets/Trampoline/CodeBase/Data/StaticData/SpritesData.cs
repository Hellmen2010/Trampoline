using UnityEngine;

namespace Trampoline.CodeBase.Data.StaticData
{
    [CreateAssetMenu(fileName = "SpritesData", menuName = "StaticData/SpritesData")]
    public class SpritesData : ScriptableObject
    {
        public Sprite OnHealth;
        public Sprite OffHealth;
    }
}