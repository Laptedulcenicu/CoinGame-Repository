using System;
using DG.Tweening;
using UnityEngine;

namespace _Coin_Game.Scripts.Game.Coin
{
    public class SwipeDetector : MonoBehaviour
    {
        [SerializeField] private bool detectSwipeOnlyAfterRelease;
        [SerializeField] private float minDistanceForSwipe = 20f;
        
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;
        private float direction;
        public float Direction => direction;


        private void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUpPosition = touch.position;
                    fingerDownPosition = touch.position;
                    direction = 0;
                }

                if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                 direction = fingerDownPosition.x - fingerUpPosition.x ;
                fingerUpPosition = fingerDownPosition;
            }
            else
            {
                direction = 0;
            }
        }
        private bool SwipeDistanceCheckMet()
        {
            return  HorizontalMovementDistance() > minDistanceForSwipe;
        }
        

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        }
        
    }

  

   
}