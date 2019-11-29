using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    private int lives;
    private TankData data;

    private static string taghold;
    public Text lifeText;
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponentInParent<TankData>();
        lifeText = GetComponent<Text>();
        taghold = data.tag;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (taghold == "Player")
        {
            lifeText.text = "Lives: " + GameManager.instance.p1Life;
        }
        else if (taghold == "Player2")
        {
            lifeText.text = "Lives: " + GameManager.instance.p2Life;
        }
    }

    public static void LifeLost()
    {
        
        //check tags and subtract life
        if (taghold == "Player")
        {
            GameManager.instance.p1Life -= 1;
            if (GameManager.instance.multiplayer)
            {
                if (GameManager.instance.p1Life < 0 && GameManager.instance.p2Life < 0)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                if (GameManager.instance.p1Life < 0)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
        //else if (taghold == "Player" && GameManager.instance.multiplayer)
        //{
        //    GameManager.instance.p1Life -= 1;
        //    // check for game over
        //    if (GameManager.instance.p1Life < 0 && GameManager.instance.p2Life < 0)
        //    {
        //        SceneManager.LoadScene(2);
        //    }
        //}
        else if (taghold == "Player2" && GameManager.instance.multiplayer)
        {
            GameManager.instance.p2Life -= 1;

            // check for game over
            if (GameManager.instance.p1Life < 0 && GameManager.instance.p2Life < 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
