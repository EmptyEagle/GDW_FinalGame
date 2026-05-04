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
    private float movementYMagnitude = 1f;
    public float gravityForce;
    private bool isGrounded;
    private float jumpInputBufferMax = 1f;
    private float jumpInputBuffer;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;
    public GameObject gameOverMenu;
    public Sprite[] loopSprites;
    public Sprite jumpSprite;
    public Sprite grazeSprite;
    public Sprite grazeJumpSprite;
    public Sprite deathSprite;
    private SpriteRenderer spriteRend;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        gameOverMenu.SetActive(false);
        isGrounded = true;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        InvokeRepeating("DecrementJumpBuffer", 0, 0.2f);
        InvokeRepeating("AnimatePlayer", 0f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        // Player horizontal movement
        if (!scoreManager.IsPaused() && !scoreManager.IsGameOver())
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

    private void AnimatePlayer()
    {
        if (isGrounded)
        {
            if (spriteRend.sprite == loopSprites[0])
            {
                spriteRend.sprite = loopSprites[1];
            }
            else if (spriteRend.sprite == loopSprites[1])
            {
                spriteRend.sprite = loopSprites[0];
            }

            if (Random.Range(0, 2) == 0)
            {
                spriteRend.flipX = !spriteRend.flipX;
            }
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
        if (other.gameObject.CompareTag("Ground") && !scoreManager.IsGameOver())
        {
            // The player falls to the ground
            isGrounded = true;
            if (spriteRend.sprite == grazeJumpSprite)
            {
                spriteRend.sprite = grazeSprite;
            }
            else
            {
                spriteRend.sprite = loopSprites[0];
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // What happens when the game is lost
            scoreManager.SetGameOver();
            spawnManager.StopWaveSpawning();
            gameOverMenu.SetActive(true);
            CancelInvoke("AnimatePlayer");
            StartCoroutine(DeathExplosion());
        }
        else if (other.gameObject.CompareTag("GrazeZone")  && !scoreManager.IsGameOver())
        {
            Time.timeScale = 0.65f;
            if (!isGrounded)
            {
                spriteRend.sprite = grazeJumpSprite;
            }
            else
            {
                spriteRend.sprite = grazeSprite;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GrazeZone") && !scoreManager.IsGameOver())
        {
            Time.timeScale = 1f;
            scoreManager.StartGraze();
            if (spriteRend.sprite == grazeJumpSprite)
            {
                spriteRend.sprite = jumpSprite;
            }
            else
            {
                spriteRend.sprite = loopSprites[0];
            }
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
            if (spriteRend.sprite == grazeSprite)
            {
                spriteRend.sprite = grazeJumpSprite;
            }
            else
            {
                spriteRend.sprite = jumpSprite;
            }
        }
        else
        {
            jumpInputBuffer = jumpInputBufferMax;
            yield return new WaitUntil(() => isGrounded);
            if (jumpInputBuffer > 0)
            {
                transform.Translate(Vector3.up * ((float)transform.position.y + movementYMagnitude));
                isGrounded = false;
                if (spriteRend.sprite == grazeSprite)
                {
                    spriteRend.sprite = grazeJumpSprite;
                }
                else
                {
                    spriteRend.sprite = jumpSprite;
                }
            }
        }
    }

    IEnumerator DeathExplosion()
    {
        spriteRend.sprite = deathSprite;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
