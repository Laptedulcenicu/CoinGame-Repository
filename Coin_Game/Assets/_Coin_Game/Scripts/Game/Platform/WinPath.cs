using UnityEngine;

namespace _Coin_Game.Scripts.Game.Platform
{
    public class WinPath : MonoBehaviour
    {
        [SerializeField] private Transform[] path;

        public Transform[] Path => path;
    }
}
