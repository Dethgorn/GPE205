using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    // you can make the public variables here for designers to use in the inspector
    public float forwardSpeed = 300.0f;
    public float reverseSpeed = 200.0f;
    public float rotateSpeed = 180.0f;

    public int shotDelay = 5;
    public int shotDamage = 1;

    public float health = 3f;
    public float maxHealth = 4f;

    public int pointValue = 100;
    public int score = 0;

}
