﻿using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;




public class BP_score : MonoBehaviour
{


    public Text scoreText;

    public GameObject ans_1;
    public GameObject tag_1;
    private string connectionString;
    private float onGoing_timer = 5f;
    private bool savedScore = false, savingScore = false;



    void Start()
    {

        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";


        if (ans_1 == null || tag_1 == null)
        {
            ans_1 = GameObject.FindGameObjectWithTag("ans_1");
            if (ans_1 != null)
            { Debug.Log("ans Find"); }

            tag_1 = GameObject.FindGameObjectWithTag("1");
            if (tag_1 != null)
            {
                Debug.Log("Tag 1");
            }

            checkTagPlace();
           



        }

    }





    void Update()
    {
        checkTagPlace();
        scoreText.text = "Score: " +  HighScore.user_onGoingtScores.ToString();
        onGoing_timer -= Time.deltaTime;
        if (onGoing_timer <= 0)
        {
            Debug.Log("going in next question");
            nextQuestion();
        }
    }


    
    

    public void checkTagPlace()
    {
        //Debug.Log("check function run");
        if (ans_1.transform.position.x == tag_1.transform.position.x)

        {
            Debug.Log("found position");
            IncrementScore();
        }


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
        if (currentScene == 8)
        {
            //it was first. 
            currentScene = 84;
        }
        else
        {
            currentScene++;
        }
        if (currentScene <= 98)
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

            //will be called only once as previous saving status will not let this to proceed
            savedScore = DBManager.sharedManager.InsertScore(HighScore.user_onGoingtScores, AppSettings.logged_in_user_id, AppSettings.selected_game_id);
            Debug.Log("score saved");
            savingScore = false;
            SceneManager.LoadScene(13);

        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Game has been finished");//Show some scene or alert here to leave from this scene becaue timer is being called

        }






    }
}