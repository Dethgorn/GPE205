using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooter : MonoBehaviour
{
    public GameObject shooter;
    public GameObject bullet;
    private Transform tf;
    private TankData data;
    private float lastShot;
    private BulletData shell;
    private string whoFired;


    // shooting stuff
    //public Shell shell;
    //public GameObject bullet;
    //shell = bullet.getComponent<Shell>();
    // shell.shooter = this.gameObject;


    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        shell = bullet.GetComponent<BulletData>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Shoot()
    {
        shell.shooter = this.gameObject;
        if ( Time.time > lastShot + data.shotDelay)
        {
            // reset the delay and shoot
            Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
            // make a noise by the listener
            AudioSource.PlayClipAtPoint(AudioController.instance.shooting, AudioController.instance.transform.position, GameManager.instance.sfxVol);
            lastShot = Time.time;
            
        }
        
    }

    public void Damage(string shotFrom)
    {
        // remove hp and if hp drops, call Die method
        data.health = data.health - data.shotDamage;

        if (data.health < 0)
        {
            whoFired = shotFrom;
            Die();
        }
    }
    // overload it for player damage
    public void Damage()
    {
        // remove hp and if hp drops, call Die method
        data.health = data.health - data.shotDamage;

        if (data.health < 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        if (whoFired == "Player")
        {
            ScoreController.ScoreUpdate(data.pointValue, whoFired);
        }
        else if (whoFired == "Player2")
        {
            ScoreController.ScoreUpdate(data.pointValue, whoFired);
        }
        AudioSource.PlayClipAtPoint(AudioController.instance.tankDeath, AudioController.instance.transform.position, GameManager.instance.sfxVol);
        // check for player tags
        
        if (gameObject.tag == "Player" || gameObject.tag == "Player2")
        {
            
            // remove a life
            LifeController.LifeLost();
            // use logic to find singleplayer or multiplayer and which player
            // respawn the player
            if(GameManager.instance.multiplayer == false && gameObject.tag == "Player" && GameManager.instance.p1Life >= 0)
            {
                PlayerSpawner.Spawner("Player", 0);
            }
            else if(GameManager.instance.multiplayer == true && gameObject.tag == "Player" && GameManager.instance.p1Life >= 0)
            {
                PlayerSpawner.Spawner("Player", 1);
            }
            else if (GameManager.instance.multiplayer == true && gameObject.tag == "Player2" && GameManager.instance.p2Life >= 0)
            {
                PlayerSpawner.Spawner("Player2", 2);
            }

        }
        Destroy(gameObject);
    }

    // using UnityEngine.SceneManagement;
    //void GameOver()
    //{

    //    gameOver = true;
    //    StartCoroutine(LoadGameOver());
    //}

    //IEnumerator LoadGameOver()
    //{

    //    yield return new WaitForSeconds(3f);
    //    SceneManager.LoadScene(2);
    //}


    
}
