using UnityEngine;

public class WaveIncrementTrigger : MonoBehaviour
{
    private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        scoreManager.IncrementWave();
    }
}
