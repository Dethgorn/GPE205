using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private GameObject spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player == null)
        {
            spawnPoint = GameManager.instance.playerSpawnPoints[Random.Range(0, GameManager.instance.playerSpawnPoints.Count)];
            Instantiate(GameManager.instance.player, spawnPoint.transform.position, Quaternion.identity);
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
