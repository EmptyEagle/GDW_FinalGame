using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    private float movementYMagnitude = 2f;
    public float gravityForce;
    private bool isGrounded;
    private float jumpInputBufferMax = 1f;
    private float jumpInputBuffer;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;
    public GameObject gameOverMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverMenu.SetActive(false);
        isGrounded = true;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        InvokeRepeating("DecrementJumpBuffer", 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Player horizontal movement
        if (!scoreManager.IsPaused())
        {
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
            
            foreach (KeyCode key in keys_Jump)
            {
                if (Input.GetKeyDown(key))
                {
                    StartCoroutine(JumpBufferCheck());
                }
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

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * gravityForce);
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
            gameOverMenu.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GrazeZone"))
        {
            Debug.Log("Graze");
        }
    }

    void DecrementJumpBuffer()
    {
        if (jumpInputBuffer > 0)
        {
            jumpInputBuffer--;
        }
    }
    
    IEnumerator JumpBufferCheck()
    {
        if (isGrounded)
        {
            transform.Translate(Vector3.up * ((float)transform.position.y + movementYMagnitude));
            isGrounded = false;
        }
        else
        {
            jumpInputBuffer = jumpInputBufferMax;
            yield return new WaitUntil(() => isGrounded);
            if (jumpInputBuffer > 0)
            {
                transform.Translate(Vector3.up * ((float)transform.position.y + movementYMagnitude));
                isGrounded = false;
            }
        }
    }
}
