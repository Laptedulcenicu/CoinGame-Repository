using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Obstacles
{
    public class ScaleSphere : MonoBehaviour
    {
        private Vector3 currentScale;

        private void Awake()
        {
            currentScale = transform.localScale;
        }

        private  void Start()
        {
            transform.DOScale(currentScale * 2f, 2f).OnComplete(() =>
            {
                transform.DOScale(currentScale, 2f).OnComplete(Start);
            });
        }
    }
}