// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //HUD Objects
        public Text HudScoreText;
        public Text HudTimeText;

        //Game Over Ui Objects
        public GameObject GameOverUi;

        //Game over
        public bool IsGameOver;

        public int PointsPerMouse = 100;

        private float _currentTime;
        private int _score;

        /// <summary>
        /// On startup
        /// </summary>
        protected void Start()
        {
            InitializeHud();
        }

        /// <summary>
        /// initializes the HUD
        /// </summary>
        public void InitializeHud()
        {
            IsGameOver = false;

            GameOverUi.SetActive(false);
            _score = 0;
            _currentTime = 0f;

            HudTimeText.text = _currentTime.ToString();
            HudScoreText.text = _score.ToString();

            StopAllCoroutines();
            StartCoroutine(StartTimer());
        }

        /// <summary>
        /// When a player catches a mouse, update the score.
        /// </summary>
        public void PlayerScored()
        {
            _score += PointsPerMouse;
            HudScoreText.text = _score.ToString();
        }

        public void DisplayGameOver()
        {
            IsGameOver = true;
            StopAllCoroutines();
            GameOverUi.SetActive(true);
        }

        /// <summary>
        /// Simple coroutine to display the current time.
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartTimer()
        {
            while (enabled)
            {
                _currentTime += Time.deltaTime;
                HudTimeText.text = Mathf.Round(_currentTime).ToString();
                yield return null;
            }
        }
    }
}