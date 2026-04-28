using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] laserSetPrefabs;
    private float[] spawnPositions = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };
    private float startDelay = 1.2f;
    private float waveRate = 2.5f;
    private int waveNumber;
    private int lastObstacle;
    private int waveType;
    private ScoreManager scoreManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        waveNumber = 0;
        lastObstacle = -1;
        waveType = -1;
        InvokeRepeating("RollRandomWave", startDelay, waveRate);
    }

    void RollRandomWave()
    {
        // Second comparison ensures turrets cannot spawn after a laser sequence
        while (waveType == lastObstacle || ((waveType == 7 || waveType == 8 || waveType == 10 || waveType == 11 || waveType == 12 || waveType == 13) && (lastObstacle == 0 || lastObstacle == 3 || lastObstacle == 6 || lastObstacle == 9)))
        {
            if (waveNumber > 53)
            {
                waveType = Random.Range(11, 22);
            }
            if (waveNumber > 42)
            {
                waveType = Random.Range(9, 21);
            }
            else if (waveNumber > 34)
            {
                waveType = Random.Range(9, 19);
            }
            else if (waveNumber > 31)
            {
                waveType = Random.Range(9, 17);
            }
            else if (waveNumber > 28)
            {
                waveType = Random.Range(6, 15);
            }
            // First button obstacle forced at wave 28
            else if (waveNumber > 27)
            {
                waveType = Random.Range(15, 17);
            }
            else if (waveNumber > 21)
            {
                waveType = Random.Range(5, 12);
            }
            else if (waveNumber > 15)
            {
                waveType = Random.Range(2, 9);
            }
            else if (waveNumber > 12)
            {
                waveType = Random.Range(2, 7);
            }
            else if (waveNumber > 8)
            {
                waveType = Random.Range(2, 5);
            }
            else if (waveNumber > 3)
            {
                waveType = Random.Range(0, 4);
            }
            else
            {
                waveType = Random.Range(0, 3);
            }
        }
        lastObstacle = waveType;
        SpawnWave(waveType);
        //Debug.Log("Spawning wave type "+waveType);
    }

    void SpawnWave(int waveType)
    {
        // Setting obstacle group to spawn
        GameObject obstaclePrefab;
        switch (waveType)
        {
            case 0:
                obstaclePrefab = obstaclePrefabs[0];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 1:
                obstaclePrefab = obstaclePrefabs[1];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 2:
                SpawnLaserSequence(0);
                break;
            case 3:
                SpawnLaserSequence(2);
                break;
            case 4:
                obstaclePrefab = obstaclePrefabs[2];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 5:
                obstaclePrefab = obstaclePrefabs[3];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 6:
                SpawnLaserSequence(1);
                break;
            case 7:
                obstaclePrefab = obstaclePrefabs[4];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 8:
                obstaclePrefab = obstaclePrefabs[5];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 9:
                SpawnLaserSequence(1);
                break;
            case 10:
                obstaclePrefab = obstaclePrefabs[6];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 11:
                obstaclePrefab = obstaclePrefabs[7];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 12:
                obstaclePrefab = obstaclePrefabs[8];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 13:
                obstaclePrefab = obstaclePrefabs[9];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 14:
                obstaclePrefab = obstaclePrefabs[10];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 15:
                obstaclePrefab = obstaclePrefabs[11];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 16:
                obstaclePrefab = obstaclePrefabs[12];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 17:
                obstaclePrefab = obstaclePrefabs[13];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 18:
                obstaclePrefab = obstaclePrefabs[14];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 19:
                obstaclePrefab = obstaclePrefabs[15];
                Instantiate(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
                break;
            case 20:
                SpawnLaserSequence(3);
                break;
            case 21:
                SpawnLaserSequence(4);
                break;
        }
        waveNumber++;
    }

    void SpawnLaserSequence(int laserType)
    {
        float zOffset;
        int gapPosition;
        int sequenceLength;
        GameObject laserPrefab;
        GameObject waveEndTrigger;

        // Normal laser sequence
        if (laserType == 0)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            sequenceLength = Random.Range(2, 5);
            for (int i = 0; i < sequenceLength; i++)
            {
                laserPrefab = laserSetPrefabs[gapPosition];
                GameObject currentSegment = Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                waveEndTrigger = currentSegment.transform.Find("WaveTrigger").gameObject;
                if (i < sequenceLength - 1)
                {
                    waveEndTrigger.SetActive(false);
                }
                // First wave (1) -> 5; Last wave (21) -> 3; Minimum gap (26) -> 2.5
                zOffset += Mathf.Max(3.5f + (1.5f - (waveNumber - 1) / 10f), 2.5f);
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
            sequenceLength = Random.Range(2, 5);
            for (int i = 0; i < sequenceLength; i++)
            {
                laserPrefab = laserSetPrefabs[gapPosition + 5];
                GameObject currentSegment = Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                waveEndTrigger = currentSegment.transform.Find("WaveTrigger").gameObject;
                if (i < sequenceLength - 1)
                {
                    waveEndTrigger.SetActive(false);
                }
                // First wave (13) -> 6.5; After first button (29) -> 4.9; Minimum gap (43) -> 3.5
                zOffset += Mathf.Max(5f + (1.5f - (waveNumber - 13) / 10f), 3.5f);
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
        // Laser sequence randomly spawning either fire jets or not per spawn
        else if (laserType == 2)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            sequenceLength = Random.Range(2, 5);
            for (int i = 0; i < sequenceLength; i++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    laserPrefab = laserSetPrefabs[gapPosition];
                }
                else
                {
                    laserPrefab = laserSetPrefabs[gapPosition + 5];
                }
                GameObject currentSegment = Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                waveEndTrigger = currentSegment.transform.Find("WaveTrigger").gameObject;
                if (i < sequenceLength - 1)
                {
                    waveEndTrigger.SetActive(false);
                }
                // First wave (4) -> 6.5; Last wave (21) -> 4.375; Minimum gap (28) -> 3.5
                zOffset += Mathf.Max(5f + (1.5f - (waveNumber - 4) / 8f), 3.5f);
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
        // Fire sequence with ceiling fire
        else if (laserType == 3)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            sequenceLength = Random.Range(2, 5);
            for (int i = 0; i < sequenceLength; i++)
            {
                laserPrefab = laserSetPrefabs[gapPosition + 10];
                GameObject currentSegment = Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                waveEndTrigger = currentSegment.transform.Find("WaveTrigger").gameObject;
                if (i < sequenceLength - 1)
                {
                    waveEndTrigger.SetActive(false);
                }
                // First wave (54) -> 6.5; Minimum gap (102) -> 3.5
                zOffset += Mathf.Max(5f + (1.5f - (waveNumber - 54) / 16f), 3.5f);
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
        // Laser sequence with chance of ceiling fire
        else if (laserType == 4)
        {
            zOffset = 0;
            gapPosition = Random.Range(0, 5);
            sequenceLength = Random.Range(2, 5);
            for (int i = 0; i < sequenceLength; i++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    laserPrefab = laserSetPrefabs[gapPosition + 5];
                }
                else
                {
                    laserPrefab = laserSetPrefabs[gapPosition + 10];
                }
                GameObject currentSegment = Instantiate(laserPrefab, new Vector3(laserPrefab.transform.position.x, laserPrefab.transform.position.y, laserPrefab.transform.position.z - zOffset), laserPrefab.transform.rotation);
                waveEndTrigger = currentSegment.transform.Find("WaveTrigger").gameObject;
                if (i < sequenceLength - 1)
                {
                    waveEndTrigger.SetActive(false);
                }
                // First wave (43) -> 6.5; Minimum gap (91) -> 3.5
                zOffset += Mathf.Max(5f + (1.5f - (waveNumber - 43) / 16f), 3.5f);
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
