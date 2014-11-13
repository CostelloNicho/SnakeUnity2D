// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class Snake : MonoBehaviour
    {
        //Head of the Snake
        public HeadSegment Head;
        
        //Body Segment Prefabs
        public GameObject BodySegment;

        //Queue of segments
        public Queue<BodySegment> Body;

        //Movement Variables
        public float MoveTime = 1f;
        private float _timeTillNextMove;


        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            Body = new Queue<BodySegment>();
        }

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            _timeTillNextMove = MoveTime;

            var startingSegments = FindObjectsOfType<BodySegment>();
            for (var i = 0; i < startingSegments.Length; ++i)
            {
                var segmentPosition = Head.transform.position;
                segmentPosition.x -= i+1;
                startingSegments[i].transform.position = segmentPosition;
                Body.Enqueue(startingSegments[i]);
            }
        }

        /// <summary>
        /// Add Segment
        /// </summary>
        private void AddSegment()
        {
            var segmentGo = Instantiate(BodySegment) as GameObject;
            var segment = segmentGo.GetComponent<BodySegment>();
            segment.MoveSegment(Body.Peek().PreviousPosition);
            Body.Enqueue(segment);
        }


        /// <summary>
        /// Move Segments
        /// </summary>
        /// <param name="headDirection"></param>
        private void MoveSegments(Direction headDirection)
        {
            Head.MoveHead(headDirection);

            if (Body.Count <= 0) return;

            var previousPosition = Head.PreviousPosition;
            foreach (var bodySegment in Body)
            {
                bodySegment.MoveSegment(previousPosition);
                previousPosition = bodySegment.PreviousPosition;
            }
        }

        // Update is called once per frame
        protected void Update()
        {
            if (_timeTillNextMove > 0)
            {
                _timeTillNextMove -= Time.deltaTime;
            }
            else
            {
                MoveSegments(InputManager.Instance.CurrentInputDirection);
                _timeTillNextMove = MoveTime;
            }
        }
    }
}