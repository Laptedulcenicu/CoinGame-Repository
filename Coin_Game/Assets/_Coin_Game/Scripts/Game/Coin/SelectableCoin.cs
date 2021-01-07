using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class SelectableCoin : MonoBehaviour
    {
        [HideInInspector] public Transform targetCoin;
        [HideInInspector] public float offset;
        [HideInInspector] public float minAngle;
        [HideInInspector] public float maxAngle;


        private void Update()
        {
            if (targetCoin != null)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(targetCoin.position.x + offset, targetCoin.position.y,
                        targetCoin.position.z + GetOffsetOnZAxis()),
                    Time.deltaTime * 20);

                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles,
                    targetCoin.rotation.eulerAngles, Time.deltaTime*20));
            }
        }

        private float GetOffsetOnZAxis()
        {
            float offsetZ = 0;
            if (targetCoin.rotation.eulerAngles.y == 0)
            {
                offsetZ = 0;
            }
            else if (targetCoin.rotation.eulerAngles.y > 0.1f)
            {
                offsetZ = (targetCoin.rotation.eulerAngles.y - 180) / (maxAngle - 180);
            }
            else if (targetCoin.rotation.eulerAngles.y < -0.1f)
            {
                offsetZ = -(targetCoin.rotation.eulerAngles.y - minAngle) / (180 - minAngle);
            }

            return offsetZ;
        }
    }
}