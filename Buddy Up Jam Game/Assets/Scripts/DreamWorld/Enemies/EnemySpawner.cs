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
            spawnTimer = spawnDelay;
            GameObject enemy = Instantiate(enemyPrefab);

            int spawnLocation = Random.Range(0, 2);
            enemy.transform.position = (spawnLocation == 0 ? rightSpawn.transform.position : leftSpawn.transform.position);
            if (spawnLocation == 0)
            {
                enemy.GetComponent<Patrol>().ToggleDirection();
            }
        }
        spawnTimer -= Time.deltaTime;
    }
}
