using UnityEngine;

public class ObstacleScroll : MonoBehaviour
{
    public float scrollSpeed;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
}
