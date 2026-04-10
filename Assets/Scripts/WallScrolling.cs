using UnityEngine;

public class WallScrolling : MonoBehaviour
{
    public float scrollSpeed;
    private BoxCollider wallCollider;
    private ScoreManager scoreManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallCollider = GetComponent<BoxCollider>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreManager.IsGameOver())
        {
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);
        }
        if (transform.position.z + 50 >= wallCollider.size.z / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
    }
}
