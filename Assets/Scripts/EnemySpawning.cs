using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnLocations;      

    public float spawnTimer = 5f;
    private float timer;//Storage
    private float firstMinuteTimer = 60f;
    public float stage1SpawnTimer = 5f;
    private float stage1Timer;//Storage - Stores time from inspector to restore timer
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
        stage1Timer = stage1SpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        firstMinuteTimer -= Time.deltaTime;

        if (firstMinuteTimer > 0f)
        {
            stage1SpawnTimer -= Time.deltaTime;
            if (stage1SpawnTimer <= 0f)
            {
                int RandomSpawn = Random.Range(0, spawnLocations.Length);
                Instantiate(enemy, spawnLocations[RandomSpawn].position, transform.rotation);
                stage1SpawnTimer = stage1Timer;
            }
        }
        if (timer <= 0 && firstMinuteTimer <= 0f)
        {
            int RandomSpawnSet1 = Random.Range(0,5);
            int RandomSpawnSet2 = Random.Range(5,10);
            Instantiate(enemy, spawnLocations[RandomSpawnSet1].position, transform.rotation);
            Instantiate(enemy, spawnLocations[RandomSpawnSet2].position, transform.rotation);
            timer = spawnTimer;

        }
        
    }
}
