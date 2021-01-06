using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private SwipeDetector swipeDetector;
        [SerializeField] private int maxAngle;
        [SerializeField] private int minAngle;
        [SerializeField] private float speedRotation;

        private Rigidbody rigidbody;
        private List<Transform> coinTailList = new List<Transform>();
        private Vector3 mVelocity;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            DOTween.SetTweensCapacity(500, 50);
        }


        private void Update()
        {
            CheckSwipeDesktop();
#if UNITY_IPHONE
      //     CheckSwipeMobileV2();
#endif

#if UNITY_ANDROID
            //        CheckSwipeMobileV2();
#endif

            transform.Translate(mVelocity.normalized * Time.deltaTime * 5);
        }

        private void CheckSwipeMobileV2()
        {
            mVelocity = Vector3.zero;
            mVelocity.x = -1f;

            if (swipeDetector.Direction != 0)
            {
                var rotation = rigidbody.rotation;

                if (swipeDetector.Direction > 0)
                {
                    mVelocity.x = (rotation.eulerAngles.x - 180) / (maxAngle - 180);
                    rigidbody.DORotate(new Vector3(rotation.eulerAngles.x, maxAngle, rotation.eulerAngles.z),
                        speedRotation, RotateMode.FastBeyond360);
                }
                else
                {
                    mVelocity.x = (rotation.eulerAngles.x - minAngle) / (180 - minAngle);
                    rigidbody.DORotate(new Vector3(rotation.eulerAngles.x, minAngle, rotation.eulerAngles.z),
                        speedRotation, RotateMode.FastBeyond360);
                }
            }
            else
            {
                rigidbody.DOKill();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
              //  other.transform.
            }
        }

        private void CheckSwipeDesktop()
        {
            mVelocity = Vector3.zero;
            mVelocity.x = -1f;

            if (Input.GetAxis("Horizontal") != 0)
            {
                var rotation = rigidbody.rotation;

                if (Input.GetAxis("Horizontal") > 0)
                {
                    mVelocity.x = (rotation.eulerAngles.x - 180) / (maxAngle - 180);

                    rigidbody.DORotate(new Vector3(rotation.eulerAngles.x, maxAngle, rotation.eulerAngles.z),
                        speedRotation).SetEase(Ease.Linear);
                }
                else
                {
                    mVelocity.x = (rotation.eulerAngles.x - minAngle) / (180 - minAngle);
                    rigidbody.DORotate(new Vector3(rotation.eulerAngles.x, minAngle, rotation.eulerAngles.z),
                        speedRotation).SetEase(Ease.Linear);
                }
            }
            else
            {
                rigidbody.DOKill();
            }
        }
    }
}