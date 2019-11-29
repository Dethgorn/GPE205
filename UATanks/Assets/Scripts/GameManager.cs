using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // hold the player and list of enemies
    public bool multiplayer;
    public float musicVol;
    public float sfxVol;
    public GameObject player;
    public GameObject player1;
    public GameObject player2;
    public List<GameObject> enemies;

    public int p1Life;
    public int p1Score;
    public int p2Life;
    public int p2Score;
    
    public List<GameObject> playerSpawnPoints = new List<GameObject>();
    public List<GameObject> enemySpawnPoints = new List<GameObject>();
    public List<GameObject> pickups = new List<GameObject>();


    // Runs before any Start() functions run
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameManager.");
            Destroy(gameObject);
        }
    }

}
