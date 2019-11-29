using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioClip shooting;
    public AudioClip shotHit;
    public AudioClip tankDeath;
    public AudioClip pickingUp;
    public AudioClip buttonPush;
    public AudioClip buttonRelease;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one AudioController.");
            Destroy(gameObject);
        }
    }

    
}
