// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    public Text ScoreText;
    public Text TimeText;

    private int _score;
    private float _currentTime;

    protected void Start()
    {
        _score = 0;
        _currentTime = 0f;

        TimeText.text = _currentTime.ToString();
        ScoreText.text = _score.ToString();

        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (enabled)
        {
            _currentTime += Time.deltaTime;
            TimeText.text = _currentTime.ToString();
            yield return null;
        }
    }

    public void PlayerScored()
    {
        _score += 100;
        ScoreText.text = _score.ToString();
    }
}