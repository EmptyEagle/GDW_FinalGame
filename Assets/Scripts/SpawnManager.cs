using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] laserSetPrefabs;
    private float[] spawnPositions = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };
    private float startDelay = 2f;
    private float waveRate = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RollRandomWave", startDelay, waveRate);
    }

    void RollRandomWave()
    {
        int waveType = Random.Range(0, 2);
        SpawnWave(waveType);
        Debug.Log("Spawning wave type "+waveType);
    }

    void SpawnWave(int waveType)
    {
        // Setting obstacle group to spawn
        switch (waveType)
        {
            case 0:
                float zOffset = 0;
                for (int i = 0; i < Random.Range(2, 5); i++)
                {
                    GameObject laserPrefab = laserSetPrefabs[Random.Range(0, 5)];
                    Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                    zOffset += 5;
                }

                break;
            case 1:
                GameObject obstaclePrefab = obstaclePrefabs[0];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
        }
    }

    public void StopWaveSpawning()
    {
        CancelInvoke("RollRandomWave");
    }
}
