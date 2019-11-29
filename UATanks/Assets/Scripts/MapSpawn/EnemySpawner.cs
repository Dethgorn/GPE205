using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // expose vars
    public float rate;
    public int waveSize = 1;
    // public GameObject[] enemies;

    private GameObject badguy;
    private GameObject spawnPoint;


    // Use this for initialization
    void Start()
    {
        // set spawn rate
        InvokeRepeating("Spawner", rate, rate);
    }

    // Update is called once per frame
    void Spawner()
    {
        for (int i = 0; i < waveSize; i++)
        {
            badguy = GameManager.instance.enemies[Random.Range(0, GameManager.instance.enemies.Count)];
            spawnPoint = GameManager.instance.enemySpawnPoints[Random.Range(0, GameManager.instance.enemySpawnPoints.Count)];

            Instantiate(badguy, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}
