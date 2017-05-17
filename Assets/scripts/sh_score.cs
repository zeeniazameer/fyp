using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;



public class AppSettings
{
    public static int logged_in_user_id;
    public static int selected_game_id;

}


public class sh_score : MonoBehaviour
{

    static int score = 0;

    public Text scoreText;
    private string connectionString;
    private float timer = 16f;
    private Text timerSeconds;

    public AudioSource source;
    public AudioClip match_sound;
    public AudioClip mis_match_sound;


    public EventSystem mySystem;
    public List<Selectable> selectables;
    private GameObject lastSelectedObject;
    private bool savedScore = false, savingScore = false;



    void Start()
    {
        score = score;


        timerSeconds = GetComponent<Text>();

        selectables = Selectable.allSelectables;
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";
        Debug.Log(connectionString);

    }


    void Update()
    {
        scoreText.text = HighScore.user_onGoingtScores.ToString();


        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            endGame();
        }


    }

   


    

    public void CheckMatch()
    {



        if (lastSelectedObject == null)
            lastSelectedObject = mySystem.currentSelectedGameObject;
        Debug.Log("Select & Checking Match..");


        if (mySystem.currentSelectedGameObject != lastSelectedObject)
        {

            //Debug.Log ("Last Selected : " + lastSelectedObject);
            //Debug.Log("Current Selected : " + mySystem.currentSelectedGameObject);

            if (mySystem.currentSelectedGameObject.name == lastSelectedObject.name)
            {

                Debug.Log("Image Match !");
                IncrementScore();
                UpdateBoardAfterSuccess();





            }
            else
            {

                Debug.Log("Images Not Matching");
               ResetBuffers();

            }

        }

    }




    void UpdateBoardAfterSuccess()
    {

        // I brutaly set the whole gameObject to unActive but you could just disable the image component
        lastSelectedObject.SetActive(false);
        mySystem.currentSelectedGameObject.SetActive(false);
        ResetBuffers();

    }

    void ResetBuffers()
    {

        lastSelectedObject = null;
        mySystem.SetSelectedGameObject(null);

    }

    public void IncrementScore()
    {

        HighScore.user_onGoingtScores += 10;
        scoreText.text = "Score: " + HighScore.user_onGoingtScores;
     
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
            Debug.Log("Game has been finished");

        }

    }

}

