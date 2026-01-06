using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public GameObject spawnArea;

    public int enemyAlive;

    int currentWaveIndex;

    public static WaveManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if(enemyAlive <= 0 && (currentWaveIndex + 1) < waves.Count)
        {
            NextWave();
        }

    }

    void NextWave()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        Wave currentWave = waves[currentWaveIndex];
        
        Vector3 spawnPoint1 = spawnPoints[0].transform.position;
        Vector3 spawnPoint2 = spawnPoints[1].transform.position;
        
        float minX = Mathf.Min(spawnPoint1.x, spawnPoint2.x);
        float maxX = Mathf.Max(spawnPoint1.x, spawnPoint2.x);

        for (int i = 0; i < currentWave.enemyPrefabs.Length; i++)
        {
            GameObject enemyPrefab = currentWave.enemyPrefabs[i];
            int quantity = currentWave.enemyQuantities[i];

            enemyAlive += quantity;

            for (int j = 0; j < quantity; j++)
            {
                float randomX = Random.Range(minX, maxX);

                GameObject enemy = Instantiate(enemyPrefab);
                enemy.transform.position = new Vector3(randomX, spawnArea.transform.position.y, 0);
                
                Vector3 rotation = enemy.transform.eulerAngles;
                rotation.z = 0;
                enemy.transform.eulerAngles = rotation;
            }
        }

        currentWaveIndex++;
    }
}
