using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class SelectableCoin : MonoBehaviour
    {
        public Transform targetCoin;
        public float offset;

        private void Update()
        {
            if (targetCoin != null)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(targetCoin.position.x + offset, targetCoin.position.y, targetCoin.position.z),
                    Time.deltaTime * 20);

                transform.LookAt(targetCoin);
            }
        }
    }
}