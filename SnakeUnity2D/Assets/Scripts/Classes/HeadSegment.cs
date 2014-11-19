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
        }

        /// <summary>
        /// Move the head of the snake according to the user input
        /// </summary>
        /// <param name="direction"></param>
        public void MoveHead(Direction direction)
        {
            SegmentDirection = direction;
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
    }
}