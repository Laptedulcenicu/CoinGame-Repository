using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Obstacles
{
    public class CubeRotation : MonoBehaviour
    {
        private Vector3 currentRotation;
        public Vector3 targetRotation;


        private void Awake()
        {
            currentRotation = transform.localScale;
        }

        private void Start()
        {
            transform.DOLocalRotate(targetRotation, 2f, RotateMode.FastBeyond360).OnComplete(() =>
            {
                transform.DOLocalRotate(currentRotation, 2f, RotateMode.FastBeyond360).OnComplete(Start);
            });
        }
    }
}