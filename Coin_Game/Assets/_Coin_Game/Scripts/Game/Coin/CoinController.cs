using System;
using System.Collections.Generic;
using System.Linq;
using _Coin_Game.Scripts.Game.Platform;
using _Coin_Game.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class CoinController : MonoBehaviour
    {
        public static Action<bool> MoveState;

        [SerializeField] private float distanceBetweenCoins;
        [SerializeField] private SwipeDetector swipeDetector;
        [SerializeField] private int maxAngle;
        [SerializeField] private int minAngle;
        [SerializeField] private float speedRotation;

        private Rigidbody rigidbody;
        private List<Transform> coinTailList = new List<Transform>();
        private Vector3 mVelocity;
        private bool canMove;

        private void Awake()
        {
            MoveState += SetMoveState;
            rigidbody = GetComponent<Rigidbody>();
            DOTween.SetTweensCapacity(500, 50);
            coinTailList.Add(transform);
        }

        private void OnDestroy()
        {
            MoveState -= SetMoveState;
        }

        private void SetMoveState(bool value)
        {
            canMove = value;
        }


        private void Update()
        {
            if (canMove)
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
        }

        private void OnTriggerEnter(Collider other)
        {
            print("iuy");
            if (other.CompareTag("Coin"))
            {
                UIManager.CoinTextValue(1);
                other.enabled = false;
                AddNewCoin(other.transform);
            }
            else if (other.CompareTag("Damage"))
            {
                print("ye[");
                UIManager.CoinTextValue(-1);

                RemoveCoin(other.gameObject);
            }
            else if (other.CompareTag("DeadZone"))
            {
                UIManager.SetGameState(GameState.Lose);
                canMove = false;
            }
            else if (other.CompareTag("Finish"))
            {
                canMove = false;

                Vector3[] pathAnimation = GetPathAnimation(other.GetComponent<WinPath>().Path);

                CoinWinAnimation(pathAnimation);
            }
        }

        private void AddNewCoin(Transform newCoin)
        {
            var currentSelectableCoin = newCoin.GetComponent<SelectableCoin>();
            
            currentSelectableCoin.targetCoin = coinTailList.Last();
            currentSelectableCoin.offset = distanceBetweenCoins;
            currentSelectableCoin.minAngle = minAngle;
            currentSelectableCoin.maxAngle = maxAngle;

            coinTailList.Add(newCoin);
        }

        private void RemoveCoin(GameObject touchedObject)
        {
            if (coinTailList.Count <= 0)
            {
                UIManager.SetGameState(GameState.Lose);
                canMove = false;
            }
            else
            {
                Destroy(coinTailList.Last().gameObject);
                coinTailList.Remove(coinTailList.Last());
                Destroy(touchedObject);
            }
        }

        private Vector3[] GetPathAnimation(Transform[] pathTransformList)
        {
            return pathTransformList.Select(transform1 => transform1.position).ToArray();
        }

        private void CoinWinAnimation(Vector3[] pathAnimation)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Insert(0, transform.DOPath(pathAnimation, 2f));
            sequence.Insert(0, transform.DORotate(new Vector3(0, 180, 0), 1.2f));
            sequence.OnComplete(() => { UIManager.SetGameState(GameState.Win); });

            foreach (var coin in coinTailList)
            {
                coin.DOScale(Vector3.zero, 0.5f).SetDelay(1f);
            }
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