using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public class sp_score : MonoBehaviour
{
    public Text scoreText;
    private string connectionString;

    private float onGoing_timer = 10f;
    private bool savedScore = false, savingScore = false;


    public GameObject ans_1;
    public GameObject tag_1;
    public GameObject ans_2;
    public GameObject tag_2;
    public GameObject ans_3;
    public GameObject tag_3;
    public int box_1 = 1;
    public int box_2 = 1;
    public int box_3 = 1;
    bool word_1 = true;
    bool word_2 = true;
    bool word_3 = true;

    public AudioSource source;
    public AudioClip click;





    public void OnClick()
    {

        source.PlayOneShot(click);

    }


    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";



        if ((ans_1 == null || tag_1 == null) || (ans_2 == null || tag_2 == null) || (ans_3 == null || tag_3 == null))
        {
            ans_1 = GameObject.FindGameObjectWithTag("ans_1");
            if (ans_1 != null)
            {
                //Debug.Log("ans Find");
            }

            tag_1 = GameObject.FindGameObjectWithTag("1");
            if (tag_1 != null)
            {
               // Debug.Log("Tag 1");
            }

            ans_2 = GameObject.FindGameObjectWithTag("ans_2");
            if (ans_2 != null)
            {
                //Debug.Log("ans Find"); 
            }

            tag_2 = GameObject.FindGameObjectWithTag("2");
            if (tag_2 != null)
            {
               // Debug.Log("Tag 2");
            }


            ans_3 = GameObject.FindGameObjectWithTag("ans_3");
            if (ans_3 != null)
            {
               // Debug.Log("ans Find");
            }

            tag_3 = GameObject.FindGameObjectWithTag("3");
            if (tag_3 != null)
            {
                //Debug.Log("Tag 3");
            }
            checkTagPlace();

        }

    }




    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 79)
            Debug.Log("Timer" + onGoing_timer);
        checkTagPlace();
        scoreText.text = HighScore.user_onGoingtScores.ToString();

        onGoing_timer -= Time.deltaTime;
        if (onGoing_timer <= 0)
        {
            nextQuestion();
        }



    }



    public void IncrementScore()
    {
        if (SceneManager.GetActiveScene().buildIndex == 79)
            Debug.Log("Timer" + onGoing_timer);
        HighScore.user_onGoingtScores += 10;
        scoreText.text = "Score: " + HighScore.user_onGoingtScores;
        if (!word_1 && !word_2 && !word_3)
            nextQuestion();
    }

    public void checkTagPlace()
    {

        switch (box_1)
        {

            case 1:
                if (ans_1.transform.position.x == tag_1.transform.position.x)
                {
                    OnClick();
                    {
                        if (word_1 == true)
                        {
                            word_1 = false;

                            IncrementScore();
                        }

                    }
                }
                break;


        }
        switch (box_2)
        {

            case 1:
                if (ans_2.transform.position.x == tag_2.transform.position.x)
                {
                    {
                        if (word_2 == true)
                        {
                            word_2 = false;

                            IncrementScore();
                        }
                    }
                }
                break;


        }


        switch (box_3)
        {

            case 1:
                if (ans_3.transform.position.x == tag_3.transform.position.x)
                {
                    {
                        if (word_3 == true)
                        {
                            word_3 = false;

                            IncrementScore();
                        }
                    }
                }
                break;





        }
    }


    public void nextQuestion()
    {

        Debug.Log("Scene Name " + SceneManager.GetActiveScene().name + "number " + SceneManager.GetActiveScene().buildIndex);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current SCene: " + currentScene);

        if (currentScene == 7)
        {
            //it was first. 
            currentScene = 73;
     

        }
        else
        {
            currentScene++;
        }
        Debug.Log("Scene to show: "+ currentScene);

        if (currentScene <= 83)
        {
            Debug.Log("Next Question");
            SceneManager.LoadScene(currentScene);
        }
        else
        {
           

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
           

        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Game has been finished");//Show some scene or alert here to leave from this scene becaue timer is being 


        }
    }
}