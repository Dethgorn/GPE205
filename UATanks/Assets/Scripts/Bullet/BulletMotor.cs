﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotor : MonoBehaviour
{
    private BulletData data;
    private Transform tf;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        data = gameObject.GetComponent<BulletData>();
        tf = gameObject.GetComponent<Transform>();
        Destroy(gameObject, data.bulletLife);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = tf.forward * data.bulletSpeed * Time.deltaTime;
        rb.velocity = moveVector;
        
    }
    

   
    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(AudioController.instance.shotHit, AudioController.instance.transform.position, GameManager.instance.sfxVol);
        if (other.gameObject.tag == "Enemy")
        {
            // hurt the enemy and destroy the bullet
            other.gameObject.GetComponent<TankShooter>().Damage(data.shooter.tag);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            // hurt the player and destroy the bullet
            other.gameObject.GetComponent<TankShooter>().Damage();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
