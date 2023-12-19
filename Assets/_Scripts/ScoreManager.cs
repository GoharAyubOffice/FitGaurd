using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Use the TMPro namespace

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI instead of Text

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public static void IncreaseScore(int amount)
    {
        score += amount;
    }

    public static void DecreaseScore(int amount)
    {
        score -= amount;
        if (score < 0) score = 0;
    }
}