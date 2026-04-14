using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] laserSetPrefabs;
    private float[] spawnPositions = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };
    private float startDelay = 2f;
    private float waveRate = 2.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("RollRandomWave", startDelay, waveRate);
    }

    void RollRandomWave()
    {
        int waveType = Random.Range(0, 6);
        SpawnWave(waveType);
        Debug.Log("Spawning wave type "+waveType);
    }

    void SpawnWave(int waveType)
    {
        // Setting obstacle group to spawn
        GameObject obstaclePrefab;
        switch (waveType)
        {
            case 0:
                SpawnLaserSequence(0);
                break;
            case 1:
                SpawnLaserSequence(1);
                break;
            case 2:
                obstaclePrefab = obstaclePrefabs[0];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 3:
                obstaclePrefab = obstaclePrefabs[1];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 4:
                obstaclePrefab = obstaclePrefabs[2];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 5:
                obstaclePrefab = obstaclePrefabs[3];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
        }
    }

    void SpawnLaserSequence(int laserType)
    {
        float zOffset;
        int gapPosition;

        // Normal laser sequence
        if (laserType == 0)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                GameObject laserPrefab = laserSetPrefabs[gapPosition];
                Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                zOffset += 3.5f;
                gapPosition = Random.Range(gapPosition - 2, gapPosition + 2);
                while (gapPosition > 4)
                {
                    gapPosition = Random.Range(2, 5);
                }
                while (gapPosition < 0)
                {
                    gapPosition = Random.Range(0, 3);
                }
            }
        }
        // Laser sequence with fire jets
        else if (laserType == 1)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                GameObject laserPrefab = laserSetPrefabs[gapPosition + 5];
                Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                zOffset += 5;
                gapPosition = Random.Range(gapPosition - 2, gapPosition + 2);
                while (gapPosition > 4)
                {
                    gapPosition = Random.Range(2, 5);
                }
                while (gapPosition < 0)
                {
                    gapPosition = Random.Range(0, 3);
                }
            }
        }
    }

    public void StopWaveSpawning()
    {
        CancelInvoke("RollRandomWave");
    }
}
