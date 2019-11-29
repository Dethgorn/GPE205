using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // functions to put on buttons for changing scenes
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame(bool multiplayer)
    {
        GameManager.instance.multiplayer = multiplayer;
        ScoreController.score = 0;
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int sceneIndex)
    {

        SceneManager.LoadScene(sceneIndex);
    }
}
