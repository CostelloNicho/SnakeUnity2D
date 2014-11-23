// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Snake : Singleton<Snake>
    {
        private readonly Vector3 _startPosition = Vector3.zero;
        public List<BodySegment> Body;
        public GameObject BodySegment;
        public HeadSegment Head;
        public float StartMoveTime = 0.3f;

        private float _currentMoveTime;
        private float _timeTillNextMove;

        protected void Awake()
        {
            Body = new List<BodySegment>();
        }

        protected void Start()
        {
            _currentMoveTime = StartMoveTime;
            InitializeSnake();
        }

        public void InitializeSnake()
        {
            Head.transform.position = _startPosition;
            foreach (BodySegment bodySegment in Body)
                Destroy(bodySegment.gameObject);
            Body.Clear();
            _currentMoveTime = StartMoveTime;
            _timeTillNextMove = _currentMoveTime;
            StartCoroutine(MovementTimer());
        }

        public void AddSegment()
        {
            var segmentPosition = Body.Count < 1 ? Head.PreviousPosition : Body.Last().PreviousPosition;
            var segmentGo = Instantiate(BodySegment) as GameObject;
            var segment = segmentGo.GetComponent<BodySegment>();
            segment.MoveSegment(segmentPosition);
            Body.Add(segment);
            _currentMoveTime = _currentMoveTime - 0.003f;
        }

        private void MoveSegments()
        {
            Head.MoveHead();
            if (Body.Count <= 0) return;
            var previousPosition = Head.PreviousPosition;
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
                    _timeTillNextMove = _currentMoveTime;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        protected void Update()
        {
            //Using half heights because we are working with a 4 coordinate plane,
            //the center of the screen in 0,0;
            var hasHitTop = Head.transform.position.y > ResolutionManager.HalfHeight - Head.Height ||
                            Head.transform.position.y < -ResolutionManager.HalfHeight + Head.Height;
            var hasHitSide = Head.transform.position.x > ResolutionManager.HalfWidth - Head.Width ||
                             Head.transform.position.x < -ResolutionManager.HalfWidth + Head.Width;
            var hasHitbody = Body.Any(segment => segment.transform.position == Head.transform.position);

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