using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private KeyCode[] keys_MoveLeft =
    {
        KeyCode.LeftArrow,
        KeyCode.A
    };
    private KeyCode[] keys_MoveRight =
    {
        KeyCode.RightArrow,
        KeyCode.D
    };
    private float movementXMagnitude = 1.1f;
    private float movementXBounds = 2.2f;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player horizontal movement
        foreach (KeyCode key in keys_MoveLeft)
        {
            if (Input.GetKeyDown(key))
            {
                transform.Translate(Vector3.left * movementXMagnitude);
            }
        }

        foreach (KeyCode key in keys_MoveRight)
        {
            if (Input.GetKeyDown(key))
            {
                transform.Translate(Vector3.right * movementXMagnitude);
            }
        }
        
        // Locking player within x-axis boundary
        if (transform.position.x < -movementXBounds)
        {
            transform.position = new Vector3(-movementXBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > movementXBounds)
        {
            transform.position = new Vector3(movementXBounds, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // What happens when the game is lost
            Debug.Log("Lose game!");
            scoreManager.SetGameOver();
            spawnManager.StopWaveSpawning();
            Destroy(gameObject);
        }
    }
}
