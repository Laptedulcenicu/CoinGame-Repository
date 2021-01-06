using UnityEngine;

namespace _Coin_Game.Scripts.Game.Platform
{
    public class ObstaclesPlatform : MonoBehaviour
    {
        [SerializeField] private Transform startTarget;
        [SerializeField] private Transform endTarget;
        public Transform StartTarget => startTarget;

        public Transform EndTarget => endTarget;
        
        
        
    }
}
