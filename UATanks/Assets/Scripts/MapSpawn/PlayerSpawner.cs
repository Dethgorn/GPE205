using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private static GameObject player;
    private static GameObject player2;
    private static GameObject spawnPoint;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.2f);
        //find GO and put the tankdata lives into the gamemanager
        if (GameManager.instance.multiplayer)
        {
            Spawner("Player", 1);
            Spawner("Player2", 2);
            player = GameObject.FindGameObjectWithTag("Player");
            GameManager.instance.p1Life = player.GetComponent<TankData>().lives;

            player2 = GameObject.FindGameObjectWithTag("Player2");
            GameManager.instance.p2Life = player2.GetComponent<TankData>().lives;
            
        }
        else
        {
            Spawner("Player", 0);
            player = GameObject.FindGameObjectWithTag("Player");
            GameManager.instance.p1Life = player.GetComponent<TankData>().lives;
        }

        //if (player.tag == "Player")
        //{
        //    GameManager.instance.p1Life = player.GetComponent<TankData>().lives;
        //}
        //else if (player2.tag == "Player2")
        //{
        //    GameManager.instance.p2Life = player2.GetComponent<TankData>().lives;
        //}
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        // old code
        //if (GameManager.instance.multiplayer)
        //{
        //    if (player == null && player2 == null)
        //    {
        //        Spawner("Player", 1);
        //        Spawner("Player2", 2);
        //    }
        //}
        //else
        //{
        //    if (player == null)
        //    {
        //        Spawner("Player", 0);
        //    }
        //}
        
    }

    public static void Spawner(string playerTag, int playernum)
    {
        spawnPoint = GameManager.instance.playerSpawnPoints[Random.Range(0, GameManager.instance.playerSpawnPoints.Count)];
        if (playernum == 0)
        {
            Instantiate(GameManager.instance.player, spawnPoint.transform.position, Quaternion.identity);
            player = GameObject.FindGameObjectWithTag(playerTag);
        }
        else if (playernum == 1)
        {
            Instantiate(GameManager.instance.player1, spawnPoint.transform.position, Quaternion.identity);
            player = GameObject.FindGameObjectWithTag(playerTag);
        }
        else if (playernum == 2)
        {
            Instantiate(GameManager.instance.player2, spawnPoint.transform.position, Quaternion.identity);
            player2 = GameObject.FindGameObjectWithTag(playerTag);
        }
        //Instantiate(GameManager.instance.player1, spawnPoint.transform.position, Quaternion.identity);
        //player = GameObject.FindGameObjectWithTag(playerTag);
    }
}
