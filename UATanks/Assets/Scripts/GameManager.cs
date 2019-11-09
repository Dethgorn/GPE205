using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // hold the player and list of enemies
    public GameObject player;
    public List<GameObject> enemies;

    public List<GameObject> playerSpawnPoints = new List<GameObject>();
    public List<GameObject> enemySpawnPoints = new List<GameObject>();
    public List<GameObject> pickups = new List<GameObject>();


    // Runs before any Start() functions run
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameManager.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // TODO: turn this into a method? 
        // pull in the spawnpoints for players and enemies into lists
        foreach (GameObject pSpawn in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            playerSpawnPoints.Add(pSpawn);
        }
        foreach (GameObject eSpawn in GameObject.FindGameObjectsWithTag("EnemyRespawn"))
        {
            enemySpawnPoints.Add(eSpawn);
        }
    }

    //void FillList(string tag)
    //{
    //    foreach (GameObject thingy in GameObject.FindGameObjectsWithTag(tag))
    //    {
    //        pickups.Add(thingy);
    //    }
    //}

}
