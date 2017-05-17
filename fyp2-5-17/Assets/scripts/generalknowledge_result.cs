using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class generalknowledge_result : MonoBehaviour
{

    static int score = 0;
    public Text scoreText;
    private string connectionString;
    private float onGoing_timer = 5f;

    private bool savedScore = false, savingScore = false;

    void Start()
    {
        score = score;
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";



    }



    void Update()
    {
        scoreText.text = "Score: " + HighScore.user_onGoingtScores.ToString();

        onGoing_timer -= Time.deltaTime;
        if (onGoing_timer <= 0)
            nextQuestion();

    }


    public void IncrementScore()
    {
        HighScore.user_onGoingtScores += 10;
        scoreText.text = "Score: " + HighScore.user_onGoingtScores;
         nextQuestion();



    }

    public void nextQuestion()
    {
       
        Debug.Log("Scene Name " + SceneManager.GetActiveScene().name + "number " + SceneManager.GetActiveScene().buildIndex);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 5)
        {
            //it was first. 
            currentScene = 53;
        }
        else
        {
            currentScene++;
        }
        if (currentScene <= 63)
        {
            Debug.Log("Next Question");
            SceneManager.LoadScene(currentScene);

        }
        else
        {
            //gamve over.

            endGame();

        }

    }
    private void endGame()
    {

        if (!savedScore && !savingScore)
        {
            savingScore = true;
            Debug.Log("Saving score");

            
            savedScore = DBManager.sharedManager.InsertScore(HighScore.user_onGoingtScores, AppSettings.logged_in_user_id, AppSettings.selected_game_id);
            Debug.Log("score saved");
            savingScore = false;
            SceneManager.LoadScene(13);

        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Game has been finished");
        }




    }
}


