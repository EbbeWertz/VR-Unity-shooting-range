using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Nodig voor UI Text

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> destroyableObjects;
    public GameObject scoreDisplay; // UI Text-object
    public GameObject target;       // Wordt later gebruikt door jou

    private int score = 0;
    private Text scoreText;

    void Start()
    {
        // Haal de Text-component op
        scoreText = scoreDisplay.GetComponent<Text>();
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    // Wordt aangeroepen door andere scripts als een object vernietigd wordt
    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }
}
