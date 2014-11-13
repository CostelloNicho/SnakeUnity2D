// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class Snake : Singleton<Snake>
    {
        //Head of the Snake
        public HeadSegment Head;
        
        //Body Segment Prefabs
        public GameObject BodySegment;

        //Queue of segments
        public List<BodySegment> Body;

        //Movement Variables
        public float MoveTime = 0.3f;
        private float _timeTillNextMove;

        //Starting
        public int StartingSegmentsCount = 3;


        /// <summary>
        /// Awake
        /// </summary>
        protected void Awake()
        {
            Body = new List<BodySegment>();
        }

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            _timeTillNextMove = MoveTime;

            for (var i = 0; i < StartingSegmentsCount; ++i)
            {
                AddSegment();
            }
        }

        /// <summary>
        /// Add Segment
        /// </summary>
        public void AddSegment()
        {
            Vector3 segmentPosition = Body.Count < 1 ? Head.PreviousPosition : Body.Last().PreviousPosition;
            var segmentGo = Instantiate(BodySegment) as GameObject;
            var segment = segmentGo.GetComponent<BodySegment>();
            segment.MoveSegment(segmentPosition);
            Body.Add(segment);
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

        /// <summary>
        /// Update
        /// </summary>
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