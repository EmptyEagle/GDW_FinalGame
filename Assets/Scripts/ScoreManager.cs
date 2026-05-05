using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Color scoreBaseColor;
    public Color scoreGrazeColor;
    public TextMeshProUGUI waveText;
    public AudioClip playerGrazeSound;
    public AudioClip playerGameOverSound;
    private AudioSource audioSource;
    private int score;
    private int wave;
    private bool isGameOver;
    private bool isPaused;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isGameOver = false;
        isPaused = false;
        score = 0;
        wave = 0;
        scoreText.text = "Score: " + score;
        waveText.text = "Wave: " + wave;
        InvokeRepeating("ScoreTimer", 1, 1);
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

    public int GetScore()
    {
        return score;
    }

    public void IncrementWave()
    {
        wave++;
        waveText.text = "Wave: " + wave;
    }

    public void StartGraze()
    {
        StartCoroutine("GrazeTick");
    }

    IEnumerator GrazeTick()
    {
        // Each graze instance increments score by a total of 5
        scoreText.color = scoreGrazeColor;
        audioSource.pitch = 0.5f;
        for (int i = 0; i < 5; i++)
        {
            AddScore(1);
            audioSource.PlayOneShot(playerGrazeSound, 0.7f);
            yield return new WaitForSeconds(0.1f);
        }
        scoreText.color = scoreBaseColor;
        audioSource.pitch = 1f;
    }

    public void SetGameOver()
    {
        isGameOver = true;
        audioSource.PlayOneShot(playerGameOverSound, 1f);
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
