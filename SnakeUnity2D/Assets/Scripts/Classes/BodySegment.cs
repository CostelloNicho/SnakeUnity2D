// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class BodySegment : MonoBehaviour
    {
        // Previous postion of the object
        public Vector3 PreviousPosition;

        /// <summary>
        /// Moves the segment by x unitys
        /// </summary>
        public void MoveSegment(Vector3 position)
        {
            PreviousPosition = transform.position;
            transform.position = position;
        }
    }
}