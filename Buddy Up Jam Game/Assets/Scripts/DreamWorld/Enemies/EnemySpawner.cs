using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject leftSpawn;
    public GameObject rightSpawn;

    public float spawnRate = 5f;
    private float spawnTimer = 0f;

    public int maxAlive = 2;
    public int spidersAlive = 0;

    public int amountToSpawn = 5;
    public int amountSpawned = 0;

    public MusicSwap musicSwap;

    public List<string> spiderMessages = new List<string>();

    private int level = -1;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (level != LevelManager.GetLevel())
        {
            level = LevelManager.GetLevel();

            DreamLevelParameters parameters = LevelInformation.Instance.GetLevelParameters(level);

            amountToSpawn = parameters.amountToSpawn;
            maxAlive = parameters.maxAlive;
            spawnRate = parameters.spawnRate;
        }
        if (spawnTimer <= 0 && spidersAlive < maxAlive && amountSpawned < amountToSpawn)
        {
            spawnTimer = spawnRate;
            GameObject enemy = Instantiate(enemyPrefab);

            //enemy.GetComponent<Patrol>().spiderMessageText.text = ( PauseMenu.Instance.triggerButton.isOn ? ":(" : spiderMessages[Random.Range(0, spiderMessages.Count)]);

            int spawnLocation = Random.Range(0, 2);
            enemy.transform.position = (spawnLocation == 0 ? rightSpawn.transform.position : leftSpawn.transform.position);
            if (spawnLocation == 0)
            {
                enemy.GetComponent<Patrol>().ToggleDirection();
            }

            spidersAlive++;
            amountSpawned++;
        }
        spawnTimer -= Time.deltaTime;
    }

    public void SpiderKilled()
    {
        spidersAlive--;
        if (amountToSpawn == amountSpawned && spidersAlive == 0)
        {
            MusicSwap.Instance.Swap();
        }
    }

    public void OutOfBoundsSpider()
    {
        amountSpawned--;
    }
}
