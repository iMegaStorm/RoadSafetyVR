using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public Transform[] carPrefabs;
    [SerializeField] int minValue;
    [SerializeField] int maxValue;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    private float time;
    private float respawnTimer;


    private void Start()
    {
        time = 0;
        SetRandomTime();

        int spawnRandomCar = Random.Range(minValue, maxValue);
        for(int x = 0; x < 1; x++)
        {
            Transform carSpawner = Instantiate(carPrefabs[spawnRandomCar], gameObject.transform.position, transform.rotation);
            carSpawner.transform.parent = gameObject.transform;
        }
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= respawnTimer)
        {
            RandomSpawn();
            SetRandomTime();
            time = 0;
        }

    }

    void SetRandomTime()
    {
        respawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }
    void RandomSpawn()
    {
        for (int x = 0; x < 1; x++)
        {
            int spawnRandomCar = Random.Range(minValue, maxValue);
            Transform carSpawner = Instantiate(carPrefabs[spawnRandomCar], gameObject.transform.position, transform.rotation);
            carSpawner.transform.parent = gameObject.transform;
        }
    }
}
