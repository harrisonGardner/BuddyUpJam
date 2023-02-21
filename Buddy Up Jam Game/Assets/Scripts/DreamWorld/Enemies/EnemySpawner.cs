using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject leftSpawn;
    public GameObject rightSpawn;

    public float spawnDelay = 5f;
    private float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            Debug.Log("Spane");
            spawnTimer = spawnDelay;
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = leftSpawn.transform.position;
        }
        spawnTimer -= Time.deltaTime;
    }
}
