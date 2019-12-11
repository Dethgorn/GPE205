using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int score;
    public static int scorep2;

    public TankData data;
    private static string tagHold;
    
    private Text scoreText;
    // private Text scoreTextp2;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponentInParent<TankData>();
        tagHold = data.tag;
        scoreText = GetComponent<Text>();

        //if (tagHold == "Player")
        //{
        //    scoreText = GetComponent<Text>();
        //    score = GameManager.instance.p1Score;
        //}
        //else if (tagHold == "Player2")
        //{
        //    scoreTextp2 = GameObject.Find("ScoreP2").GetComponent<Text>();
        //    scorep2 = GameManager.instance.p2Score;
            
        //}
    }

    private void Update()
    {
        // update the scores
        if (tagHold == "Player")
        {
            scoreText.text = "Score: " + GameManager.instance.p1Score;
        }
        else if (tagHold == "Player2")
        {
            scoreText.text = "Score:" + GameManager.instance.p2Score;
        }

    }

    // updating and saving scores
    public static void ScoreUpdate(int points, string who)
    {
        // add points for the player and store it in the gamemanager
        if (who == "Player")
        {
            score += points;
            GameManager.instance.p1Score = score;
        }
        else if (who == "Player2")
        {
            scorep2 += points;
            GameManager.instance.p2Score = scorep2;
        }
    }
}
