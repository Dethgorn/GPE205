using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int score;
    
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (this.tag == "Player")
        {
            score = GameManager.instance.p1Score;
        }
        else if (this.tag == "Player2")
        {
            score = GameManager.instance.p2Score;
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public static void ScoreUpdate(int points)
    {
        score += points;
    }
}
