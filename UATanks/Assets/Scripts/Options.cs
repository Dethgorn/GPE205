﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioSource menuMusic;
   
    private void Start()
    {
        GameManager.instance.sfxVol = PlayerPrefs.GetFloat("SFXVolume");
        GameManager.instance.musicVol = PlayerPrefs.GetFloat("MusicVolume");
        menuMusic.volume = GameManager.instance.musicVol;


    }

    public void LowMusicVolume()
    {
        menuMusic.volume -= .1f;
        GameManager.instance.musicVol = menuMusic.volume;
    }

    public void UpMusicVolume()
    {
        menuMusic.volume += .1f;
        GameManager.instance.musicVol = menuMusic.volume;
    }

    public void LowSFXVolume()
    {
        GameManager.instance.sfxVol -= .1f;
    }

    public void UpSFXVolume()
    {
        GameManager.instance.sfxVol += .1f;
    }

    public void OnToggle(bool selection)
    {
        GameManager.instance.multiplayer = selection;
    }

    public void DailyMap(Toggle choice)
    {
        GameManager.instance.mapOfTheDay = choice.isOn;
    }
    
    // save it when back button is clicked
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("SFXVolume", GameManager.instance.sfxVol);
        PlayerPrefs.SetFloat("MusicVolume", GameManager.instance.musicVol);
        PlayerPrefs.Save();
    }
}
