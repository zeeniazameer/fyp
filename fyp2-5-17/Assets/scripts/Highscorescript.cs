using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Highscorescript : MonoBehaviour {

    public GameObject username;
    public GameObject gamename;
    public GameObject score;

    public void SetScores(string Username , string Gamename , int Score)
    {
        this.username.GetComponent<Text>().text = Username;
        this.gamename.GetComponent<Text>().text = Gamename;
        this.score.GetComponent<Text>().text    = "" + Score;

       
    }

  
}
