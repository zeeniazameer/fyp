using Mono.Data.Sqlite;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


 class HighScore
    {

    public static int user_onGoingtScores;
   // public static float onGoing_timer = 10f;
    
    public int Score { get; set; }


    public string Username { get; set; }


    public string Gamename { get; set; }
    public HighScore()
    {
    }
    public HighScore( int score ,string username, string gamename) {
        this.Score = score;
        this.Username = username;
        this.Gamename = gamename;

    }
}

