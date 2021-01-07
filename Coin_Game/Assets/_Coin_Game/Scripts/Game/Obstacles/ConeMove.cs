using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Obstacles
{
    public class ConeMove : MonoBehaviour
    {
        private Vector3 currentScale;

        private void Awake()
        {
            currentScale = transform.localPosition;
        }

        private  void Start()
        {
            transform.DOLocalMove( new  Vector3(currentScale.x,currentScale.y-5,currentScale.z), 2f).OnComplete(() =>
            {
                transform.DOLocalMove(currentScale, 2f).OnComplete(Start);
            });
        }
    }
}
