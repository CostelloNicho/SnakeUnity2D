// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    internal class InputManager : Singleton<InputManager>
    {
        public Direction CurrentInputDirection;

        protected void Start()
        {
            CurrentInputDirection = Direction.Right;
        }

        protected void Update()
        {
            bool right = Input.GetKeyDown(KeyCode.RightArrow);
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if (right && CurrentInputDirection != Direction.Left)
                CurrentInputDirection = Direction.Right;
            else if (left && CurrentInputDirection != Direction.Right)
                CurrentInputDirection = Direction.Left;
            else if (up && CurrentInputDirection != Direction.Down)
                CurrentInputDirection = Direction.Up;
            else if (down && CurrentInputDirection != Direction.Up)
                CurrentInputDirection = Direction.Down;
        }
    }
}