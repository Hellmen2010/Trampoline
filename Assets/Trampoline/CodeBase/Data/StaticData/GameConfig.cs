using UnityEngine;

namespace Trampoline.CodeBase.Data.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int TopPanelSizeIphone;
        public int TopPanelSizeIpad;
        public int TimerBeforeStart;
        public int MaxHealth;
        
        [Header("Physics")]
        public float BallsSpawnDelay;
        public float YAddForcePoint;
        public float BallMoveDownTime;
        public float BallThrowForceMin;
        public float BallThrowForceMax;
        
        [Header("GameField")]
        public float LeftBoundary;
        public float RightBoundary;
        public float TrampolineMoveSpeed;
    }
}