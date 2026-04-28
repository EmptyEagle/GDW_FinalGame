using UnityEngine;
using TMPro;
using System.Collections;

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

    public void StartGraze()
    {
        StartCoroutine("GrazeTick");
    }

    IEnumerator GrazeTick()
    {
        // Each graze instance increments score by a total of 5
        for (int i = 0; i < 5; i++)
        {
            AddScore(1);
            yield return new WaitForSeconds(0.1f);
        }
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
