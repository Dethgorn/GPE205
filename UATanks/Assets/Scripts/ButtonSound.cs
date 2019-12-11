using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{

    public void OnClick()
    {
        AudioSource.PlayClipAtPoint(AudioController.instance.buttonPush, AudioController.instance.transform.position, GameManager.instance.sfxVol);
    }

    public void OnRelease()
    {
        AudioSource.PlayClipAtPoint(AudioController.instance.buttonRelease, AudioController.instance.transform.position, GameManager.instance.sfxVol);
    }
}
