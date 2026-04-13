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
    private KeyCode[] keys_Jump =
    {
        KeyCode.UpArrow,
        KeyCode.W,
        KeyCode.Space
    };
    private float movementXMagnitude = 1.1f;
    private float movementXBounds = 2.2f;
    private float movementYMagnitude = 1.5f;
    private bool isGrounded;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = true;
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

        foreach (KeyCode key in keys_Jump)
        {
            if (Input.GetKeyDown(key) && isGrounded)
            {
                transform.Translate(Vector3.up * ((float)transform.position.y + movementYMagnitude));
                isGrounded = false;
            }
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // The player falls to the ground
            isGrounded = true;
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
