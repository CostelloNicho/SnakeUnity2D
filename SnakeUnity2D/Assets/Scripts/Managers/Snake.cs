// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Snake : Singleton<Snake>
    {
        //Head of the Snake
        public HeadSegment Head;

        //Segments
        public List<BodySegment> Body;
        public GameObject BodySegment;

        //Movement Variables
        public float MoveTime = 0.3f;

        private readonly Vector3 _startPosition = Vector3.zero;
        private float _timeTillNextMove;

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
            InitializeSnake();
        }

        public void InitializeSnake()
        {
            Head.transform.position = _startPosition;
            foreach (BodySegment bodySegment in Body)
                Destroy(bodySegment.gameObject);
            Body.Clear();
            _timeTillNextMove = MoveTime;
            StartCoroutine(MovementTimer());
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
			MoveTime = MoveTime - 0.003f;
        }

        /// <summary>
        /// Move Segments
        /// </summary>
        /// <param name="headDirection"></param>
        private void MoveSegments()
        {
            Head.MoveHead();
            if (Body.Count <= 0) return;
            Vector3 previousPosition = Head.PreviousPosition;
            foreach (BodySegment bodySegment in Body)
            {
                bodySegment.MoveSegment(previousPosition);
                previousPosition = bodySegment.PreviousPosition;
            }
        }

        protected IEnumerator MovementTimer()
        {
            while (enabled)
            {
                if (_timeTillNextMove > 0)
                {
                    _timeTillNextMove -= Time.deltaTime;
                }
                else
                {
                    MoveSegments();
                    _timeTillNextMove = MoveTime;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update()
        {
            //Using half heights because we are working with a 4 coordinate plane,
            //the center of the screen in 0,0;
            bool hasHitTop = Head.transform.position.y > ResolutionManager.HalfHeight - Head.Height ||
                             Head.transform.position.y < -ResolutionManager.HalfHeight + Head.Height;
            bool hasHitSide = Head.transform.position.x > ResolutionManager.HalfWidth - Head.Width ||
                              Head.transform.position.x < -ResolutionManager.HalfWidth + Head.Width;
			bool hasHitbody = Body.Any(segment => segment.transform.position == Head.transform.position);

            if (hasHitTop || hasHitSide || hasHitbody)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            StopAllCoroutines();
            UiManager.Instance.DisplayGameOver();
        }

    }
}