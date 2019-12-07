using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int score;
    public static int scorep2;

    private TankData data;
    private static string tagHold;
    
    public Text scoreText;
    public Text scoreTextp2;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponentInParent<TankData>();
        tagHold = data.tag;

        if (tagHold == "Player")
        {
            scoreText = GetComponent<Text>();
            score = GameManager.instance.p1Score;
        }
        else if (tagHold == "Player2")
        {
            scoreTextp2 = GameObject.Find("ScoreP2").GetComponent<Text>();
            scorep2 = GameManager.instance.p2Score;
            
        }
    }

    private void Update()
    {

        //if (tagHold == "Player")
        //{

        //    UIUpdate(1);
        //}
        //if (tagHold == "Player2")
        //{
        //    UIUpdate(2);
        //}
        //scoreText.text = "Score: " + score;

        if (tagHold == "Player")
        {
            scoreText.text = "Score: " + score;
        }
        if (tagHold == "Player2")
        {
            //Debug.Log(scoreTextp2.text);
            // i get a NULL object reference but it works?
            // i do not under stand
            scoreTextp2.text = "Score:" + scorep2;
        }

    }

    public void UIUpdate(int player)
    {
        if (player == 1)
        {
            scoreText.text = "Score:" + score;
        }
        else if (player == 2)
        {
            scoreTextp2.text = "WHAT";
        }
    }
    //// save scores
    //GameManager.instance.p1Score = ScoreController.score;
    //        GameManager.instance.p2Score = ScoreController.scorep2;

    public static void ScoreUpdate(int points, string who)
    {
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
