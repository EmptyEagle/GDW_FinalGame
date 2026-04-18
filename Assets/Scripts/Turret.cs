using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireDelay;
    private bool canFire;
    private GameObject player;
    private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        canFire = true;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        // Continuously rotate turret toward player
        if (player != null)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = lookRotation;
        }
        
        // Fire until turret gets too close
        if (canFire && transform.position.z < -5 && !scoreManager.IsGameOver())
        {
            StartCoroutine("FireAtPlayer");
        }
    }

    IEnumerator FireAtPlayer()
    {
        canFire = false;

        // Fire a projectile at the player
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(fireDelay);

        canFire = true;
    }
}
