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
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        SetScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        
        if ( Time.time > lastShot + data.shotDelay)
        {
            // reset the delay and shoot
            
            Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
            lastShot = Time.time;

        }
        
    }

    public void Damage()
    {
        // remove hp and if hp drops, call Die method
        data.health = data.health - data.shotDamage;

        if (data.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // remove the GO and add the point value to score
        Destroy(gameObject);
        data.score += data.pointValue;
        SetScore();
    }

    void SetScore()
    {
        scoreText.text = "Score: " + data.score.ToString();
    }
}
