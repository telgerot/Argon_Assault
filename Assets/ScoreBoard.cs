using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score;
    Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();        
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
