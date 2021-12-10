﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointPerSecond;

    public bool scoreIncreasing;

    public bool shouldDouble;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointPerSecond * Time.deltaTime;
        }

        if(scoreCount>highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        hiScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void AddScore(int pointsAward)
    {
        if (shouldDouble)
        {
            pointsAward = pointsAward * 2;
        }
        
        scoreCount += pointsAward;
    }

}
