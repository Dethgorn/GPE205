using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : MonoBehaviour
{
    public GameObject shooter;
    public GameObject bullet;
    private Transform tf;
    private TankData data;
    private int delay;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        
    }

    // Update is called once per frame
    void Update()
    {
        delay++;
    }

    public void Shoot()
    {
        
        if ( delay > data.shotDelay)
        {
            // reset the delay and shoot
            delay = 0;
            Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
            

        }
        
    }

    public void Damage()
    {
        data.health--;

        if (data.health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        data.score += data.pointValue;
        Destroy(gameObject);
    }
}
