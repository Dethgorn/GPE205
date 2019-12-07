using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        gameMusic.volume = GameManager.instance.musicVol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
