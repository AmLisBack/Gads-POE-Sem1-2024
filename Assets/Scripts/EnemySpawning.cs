using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnLocations;      

    public float spawnTimer = 3f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            int RandomSpawnSet1 = Random.Range(0,5);
            int RandomSpawnSet2 = Random.Range(5,10);
            Instantiate(enemy, spawnLocations[RandomSpawnSet1].position, transform.rotation);
            Instantiate(enemy, spawnLocations[RandomSpawnSet2].position, transform.rotation);
            timer = spawnTimer;

        }
        if(timer < 0 )
        {
            for( int i = 0; i < spawnLocations.Length; i++ )
            {

                Instantiate(enemy, spawnLocations[i].position, transform.rotation);
            }
            timer = spawnTimer;
        }
    }
}
