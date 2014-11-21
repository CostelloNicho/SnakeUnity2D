// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class HeadSegment : MonoBehaviour
    {
        public Vector3 PreviousPosition;
        public float Width;
        public float Height;

        public Direction SegmentDirection { get; set; }

        protected void Start()
        {
            Width = GetComponent<BoxCollider2D>().bounds.size.x;
            Height = GetComponent<BoxCollider2D>().bounds.size.y;
            InputManager.Instance.DirectionChanged += HandleChangeOfDirection;
        }

        /// <summary>
        /// Move the head of the snake according to the user input
        /// </summary>
        /// <param name="direction"></param>
        public void MoveHead()
        {
            PreviousPosition = transform.position;
            Vector3 headPostion = PreviousPosition;
            switch (SegmentDirection)
            {
                case Direction.Right:
                    headPostion.x += 0.5f;
                    break;
                case Direction.Left:
                    headPostion.x -= 0.5f;
                    break;
                case Direction.Up:
                    headPostion.y += 0.5f;
                    break;
                case Direction.Down:
                    headPostion.y -= 0.5f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            PreviousPosition = transform.position;
            transform.position = headPostion;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Mouse")
            {
                UiManager.Instance.PlayerScored();
                Snake.Instance.AddSegment();
                Destroy(other.gameObject);
                Spawner.Instance.SpawnMouse();
            }
        }

        protected void HandleChangeOfDirection(object sender, SnakeInputEventArgs eventArgs)
        {
            switch (eventArgs.Direction)
            {
                case Direction.Right:
                    if(SegmentDirection != Direction.Left)
                        SegmentDirection = Direction.Right;
                    break;
                case Direction.Left:
                    if (SegmentDirection != Direction.Right)
                        SegmentDirection = Direction.Left;
                    break;
                case Direction.Up:
                    if (SegmentDirection != Direction.Down)
                        SegmentDirection = Direction.Up;
                    break;
                case Direction.Down:
                    if (SegmentDirection != Direction.Up)
                        SegmentDirection = Direction.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}