﻿// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    internal class InputManager : Singleton<InputManager>
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

        /// <summary>
        /// Update
        ///     Begins polling for the appropriate input
        /// </summary>
        private void Update()
        {
#if UNITY_IPHONE
                PollTouchInput();
            #else
            PollKeyboardInput();
#endif
        }

        /// <summary>
        /// Poll Keyboard Input
        ///     Poll the keyboard input
        /// </summary>
        protected void PollKeyboardInput()
        {
            bool right = Input.GetKeyDown(KeyCode.RightArrow);
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if (right && Snake.Instance.Head.SegmentDirection != Direction.Left)
                CurrentInputDirection = Direction.Right;
            else if (left && Snake.Instance.Head.SegmentDirection != Direction.Right)
                CurrentInputDirection = Direction.Left;
            else if (up && Snake.Instance.Head.SegmentDirection != Direction.Down)
                CurrentInputDirection = Direction.Up;
            else if (down && Snake.Instance.Head.SegmentDirection != Direction.Up)
                CurrentInputDirection = Direction.Down;

            if (!UiManager.Instance.IsGameOver) return;
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            Snake.Instance.InitializeSnake();
            UiManager.Instance.InitializeHud();
        }

        /// <summary>
        /// PollTouchInput 
        ///     Coroutine to poll input form the touch device
        ///     Only runs once per frame
        /// </summary>
        protected void PollTouchInput()
        {
            if (Input.touchCount > 0)
            {
                //only grab the first touch as we are not dealing with multitouch
                Touch touch = Input.touches[0];

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
        }

        /// <summary>
        /// 
        /// </summary>
        private void HandleInputEnded()
        {
            Direction dir = CurrentInputDirection;

            if (CheckForSwipe()) // if it is a swipe
            {
                if (Mathf.Abs(_touchStart.x - _touchEnd.x) >
                    Mathf.Abs(_touchStart.y - _touchEnd.y))
                {
                    Debug.Log("horizontal swipe");
                    dir = Mathf.Sign(_touchEnd.x - _touchStart.x) > 0 ? Direction.Right : Direction.Left;
                }
                else
                {
                    Debug.Log("verticle swipe");
                    dir = Mathf.Sign(_touchEnd.y - _touchStart.y) > 0 ? Direction.Up : Direction.Down;
                }
            }


            if (dir == Direction.Right && Snake.Instance.Head.SegmentDirection != Direction.Left)
                CurrentInputDirection = Direction.Right;
            else if (dir == Direction.Left && Snake.Instance.Head.SegmentDirection != Direction.Right)
                CurrentInputDirection = Direction.Left;
            else if (dir == Direction.Up && Snake.Instance.Head.SegmentDirection != Direction.Down)
                CurrentInputDirection = Direction.Up;
            else if (dir == Direction.Down && Snake.Instance.Head.SegmentDirection != Direction.Up)
                CurrentInputDirection = Direction.Down;
        }

        /// <summary>
        /// Checks to see if enough time has gone by for the input
        /// to be considered a swipe.
        /// </summary>
        /// <returns></returns>
        private bool CheckForSwipe()
        {
            if (_touchTime > MinSwipeTime && _touchTime < MaxSwipeTime)
                return true;
            return false;
        }
    }
}