using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // you can make the public variables here for designers to use in the inspector
    [Header("Movement")]
    public float forwardSpeed = 300.0f;
    public float reverseSpeed = 200.0f;
    public float rotateSpeed = 180.0f;
    [Header("Shooting")]
    public int shotDelay = 3;
    public int shotDamage = 1;
    [Header("HP")]
    public float health = 3f;
    public float maxHealth = 4f;
    public int lives = 3;
    [Header("Scoring")]
    public int pointValue = 100;
    public int score = 0;

}
