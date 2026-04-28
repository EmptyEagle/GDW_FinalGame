using UnityEngine;

public class ObstacleScroll : MonoBehaviour
{
    public float scrollSpeed;
    private ScoreManager scoreManager;
    private GameObject waveEndTrigger;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        waveEndTrigger = transform.Find("WaveTrigger").gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!scoreManager.IsGameOver())
        {
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }
        if (transform.position.z > 25)
        {
            Destroy(gameObject);
        }
    }

    public GameObject getWaveEndTrigger()
    {
        return waveEndTrigger;
    }
}
