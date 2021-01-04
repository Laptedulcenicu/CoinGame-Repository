using UnityEngine;

namespace _Coin_Game.Scripts.Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        void Update()
        {
            transform.position = target.position;
        }
    }
}
