using System.Collections.Generic;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private int maxAngle;
        [SerializeField] private int minAngle;
        [SerializeField] private float speedRotation;

        private Rigidbody rigidbody;
        private List<Transform> coinTailList = new List<Transform>();
        private SwipeData swipeData = new SwipeData {Direction = SwipeDirection.None};

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            SwipeDetector.onSwipe += SwipeDetector_OnSwipe;
        }


        private void Update()
        {
            rigidbody.velocity=-transform.right*5;
#if UNITY_EDITOR
            CheckSwipeDesktop();

#endif

#if UNITY_ANDROID
            CheckSwipeMobile();
#endif

#if UNITY_IOS
                CheckSwipeMobile();
#endif
        }

        private void CheckSwipeMobile()
        {
            if (swipeData.Direction != SwipeDirection.None)
            {
                var rotation = rigidbody.rotation;
                if (swipeData.Direction == SwipeDirection.Left)
                {
                    rigidbody.rotation = Quaternion.Euler(Vector3.MoveTowards(rotation.eulerAngles,
                        new Vector3(rotation.eulerAngles.x, maxAngle, rotation.eulerAngles.z),
                        Time.deltaTime * speedRotation));
                }
                else if (swipeData.Direction == SwipeDirection.Right)
                {
                    rigidbody.rotation = Quaternion.Euler(Vector3.MoveTowards(rotation.eulerAngles,
                        new Vector3(rotation.eulerAngles.x, minAngle, rotation.eulerAngles.z),
                        Time.deltaTime * speedRotation));
                }
            }
        }

        private void CheckSwipeDesktop()
        {
            var rotation = rigidbody.rotation;
            if (Input.GetAxis("Horizontal") > 0)
            {
                rigidbody.rotation = Quaternion.Euler(Vector3.MoveTowards(rotation.eulerAngles,
                    new Vector3(rotation.eulerAngles.x, maxAngle, rotation.eulerAngles.z),
                    Time.deltaTime * speedRotation));
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rigidbody.rotation = Quaternion.Euler(Vector3.MoveTowards(rotation.eulerAngles,
                    new Vector3(rotation.eulerAngles.x, minAngle, rotation.eulerAngles.z),
                    Time.deltaTime * speedRotation));
            }
        }

        private void SwipeDetector_OnSwipe(SwipeData data)
        {
            swipeData = data;
        }
    }
}