// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //HUD Objects
        public GameObject GameOverUi;
        public Text HudScoreText;
        public Text HudTimeText;

        private TimeSpan _timeSpan;
        private int _score;

        protected void Start()
        {
            InitializeHud();
        }

        protected void OnEnable()
        {
            Messenger<int>.AddListener(SnakeEvents.PlayerScored, OnPlayerScored);
            Messenger.AddListener(SnakeEvents.GameOver, OnGameOver);
        }

        protected void OnDisable()
        {
            Messenger<int>.RemoveListener(SnakeEvents.PlayerScored, OnPlayerScored);
            Messenger.RemoveListener(SnakeEvents.GameOver, OnGameOver);
        }

        public void InitializeHud()
        {
            GameOverUi.SetActive(false);
            _score = 0;
            _timeSpan = TimeSpan.Zero;

            UpdateDisplay();
            StartCoroutine(StartTimer());
        }

        public void OnPlayerScored(int points)
        {
            _score += points;
            UpdateDisplay();
        }

        public void OnGameReset()
        {
            Messenger.Broadcast(SnakeEvents.ResetGame);
            InitializeHud();
        }

        public void OnGameOver()
        {
            StopAllCoroutines();
            GameOverUi.SetActive(true);
        }

        private void UpdateDisplay()
        {
            HudScoreText.text = string.Format("Score: {0}", _score);
            HudTimeText.text = string.Format("{0}:{1}:{2}", 
                _timeSpan.Minutes, _timeSpan.Seconds, _timeSpan.Milliseconds);

        }


        private IEnumerator StartTimer()
        {
            while (enabled)
            {
               _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(Time.deltaTime));
                UpdateDisplay();
                yield return null;
            }
        }
    }
}