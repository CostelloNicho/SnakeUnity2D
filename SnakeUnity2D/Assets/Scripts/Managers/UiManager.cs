﻿// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //HUD Objects
        private const int PointsPerMouse = 100;
        public GameObject GameOverUi;
        public Text HudScoreText;
        public Text HudTimeText;

        public bool IsGameOver;

        private float _currentTime;
        private int _score;

        protected void Start()
        {
            InitializeHud();
        }

        public void InitializeHud()
        {
            IsGameOver = false;
            GameOverUi.SetActive(false);
            _score = 0;
            _currentTime = 0f;

            UpdateDisplay();
            StartCoroutine(StartTimer());
        }

        public void PlayerScored()
        {
            _score += PointsPerMouse;
            UpdateDisplay();
        }

        public void DisplayGameOver()
        {
            IsGameOver = true;
            StopAllCoroutines();
            GameOverUi.SetActive(true);
        }

        private void UpdateDisplay()
        {
            HudScoreText.text = string.Format("Score: {0}", _score);
            HudTimeText.text = string.Format("Time: {0}", Mathf.Round(_currentTime));
        }


        private IEnumerator StartTimer()
        {
            while (enabled)
            {
                _currentTime += Time.deltaTime;
                UpdateDisplay();
                yield return null;
            }
        }
    }
}