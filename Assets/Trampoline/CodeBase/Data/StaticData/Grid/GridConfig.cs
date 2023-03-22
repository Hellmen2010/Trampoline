using UnityEngine;

namespace Trampoline.CodeBase.Data.StaticData.Grid
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "StaticData/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        public int Columns;
        public int Rows;
        public float TopPadding;
        public float BottomPadding;
        public float LeftPadding;
        public float RightPadding;
        public float YSpacing;
        public float XSpacing;
    }
}