using Trampoline.CodeBase.Core.Element;
using UnityEngine;

namespace Trampoline.CodeBase.Data.StaticData
{
    [CreateAssetMenu(fileName = "ElementData", menuName = "StaticData/ElementData")]
    public class ElementData : ScriptableObject
    {
        public Element[] Elements;
    }
}