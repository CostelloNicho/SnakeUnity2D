// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        //Touch Variables
        private const float MinSwipeTime = 0.06f;
        private const float MaxSwipeTime = 1.0f;
        public Direction CurrentInputDirection;
        private Vector2 _touchEnd;
        private Vector2 _touchStart;
        private float _touchTime;

        protected void Start()
        {
            CurrentInputDirection = Direction.Right;
        }

        protected void PollKeyboardInput()
        {
            var right = Input.GetKeyDown(KeyCode.RightArrow);
            var left = Input.GetKeyDown(KeyCode.LeftArrow);
            var up = Input.GetKeyDown(KeyCode.UpArrow);
            var down = Input.GetKeyDown(KeyCode.DownArrow);

            if (right)
                CurrentInputDirection = Direction.Right;
            else if (left)
                CurrentInputDirection = Direction.Left;
            else if (up)
                CurrentInputDirection = Direction.Up;
            else if (down)
                CurrentInputDirection = Direction.Down;
            OnDirectionChange();

        }

        protected void OnDirectionChange()
        {
           var args = new SnakeInputEventArgs(CurrentInputDirection);
           Messenger<SnakeInputEventArgs>.Broadcast(
               SnakeEvents.DirectionChanged, args);
        }

        protected void PollTouchInput()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                    //on begin of a single touch get the position a time 
                    //as well as check for an object being touched
                case TouchPhase.Began:
                    _touchStart = touch.position;
                    _touchTime = Time.time;
                    break;

                    //If the user is touching the ball release it from
                    //the character and start following their touch
                case TouchPhase.Moved:
                    break;

                    //Not implementing a stationary gensture
                    //Idea: if user is touching the character have him wave.
                case TouchPhase.Stationary:
                    break;

                    //When the touch ends we need to calculate if it were a swipe, that is
                    // only if the player was not holding the ball, in that case we want 
                    // to just release the ball.
                case TouchPhase.Ended:
                    _touchEnd = touch.position;
                    _touchTime = Time.time - _touchTime;

                    HandleInputEnded();
                    break;

                    // Reset the touch variables
                case TouchPhase.Canceled:
                    _touchEnd = Vector3.zero;
                    _touchTime = 0f;
                    break;
            }
        }

        private void HandleInputEnded()
        {
            var dir = CurrentInputDirection;

            if (CheckForSwipe()) // if it is a swipe
            {
                if (Mathf.Abs(_touchStart.x - _touchEnd.x) >
                    Mathf.Abs(_touchStart.y - _touchEnd.y))
                {
                    Debug.Log("horizontal swipe");
                    dir = Mathf.Sign(_touchEnd.x - _touchStart.x) > 0 
                        ? Direction.Right : Direction.Left;
                }
                else
                {
                    Debug.Log("verticle swipe");
                    dir = Mathf.Sign(_touchEnd.y - _touchStart.y) > 0 
                        ? Direction.Up : Direction.Down;
                }
            }


            if (dir == Direction.Right)
                CurrentInputDirection = Direction.Right;
            else if (dir == Direction.Left)
                CurrentInputDirection = Direction.Left;
            else if (dir == Direction.Up)
                CurrentInputDirection = Direction.Up;
            else if (dir == Direction.Down)
                CurrentInputDirection = Direction.Down;
            OnDirectionChange();
        }

        /// <summary>
        /// Checks to see if enough time has gone by for the input
        /// to be considered a swipe.
        /// </summary>
        /// <returns></returns>
        private bool CheckForSwipe()
        {
            return _touchTime > MinSwipeTime && _touchTime < MaxSwipeTime;
        }


        private void Update ()
        {
#if UNITY_IPHONE
                PollTouchInput();
#else
            PollKeyboardInput();
#endif
        }
    }
}