using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private bool isGameOver;
    private bool isPaused;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        isPaused = false;
        score = 0;
        scoreText.text = "Score: " + score;
        InvokeRepeating("ScoreTimer", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            CancelInvoke("ScoreTimer");
        }
    }

    void ScoreTimer()
    {
        AddScore(1);
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetPaused(bool pausedState)
    {
        isPaused = pausedState;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
