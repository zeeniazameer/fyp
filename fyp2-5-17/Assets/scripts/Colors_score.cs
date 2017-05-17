using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Colors_score : MonoBehaviour {

    static int score = 0;
    public Text scoreText;
    private float onGoing_timer = 12f;
    private string connectionString;
    public EventSystem mySystem;
    public List<Selectable> selectables;
    private GameObject lastSelectedObject;
    public AudioSource source;
    public AudioClip match;
    public AudioClip notmatch;
    private bool savedScore = false, savingScore = false;




    // Use this for initialization
    void Start(){
        score = score;
        selectables = Selectable.allSelectables;
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";
      



}


    void Update()
    {
        
        scoreText.text = "Score: " + HighScore.user_onGoingtScores.ToString();

        onGoing_timer -= Time.deltaTime;
        if (onGoing_timer <= 0)
        {
            nextQuestion();
        }

    }


    public void matching()
    {

        source.PlayOneShot(match);

    }



    public void not_matching()
    {

        source.PlayOneShot(notmatch);

    }

   

    public void CheckMatch()
    {


        if (lastSelectedObject == null)
            lastSelectedObject = mySystem.currentSelectedGameObject;
        Debug.Log("Select & Checking Match..");


        if (mySystem.currentSelectedGameObject != lastSelectedObject)
        {

          
            if (mySystem.currentSelectedGameObject.name == lastSelectedObject.name)
            {

                Debug.Log ("Image Match !");
                IncrementScore();
          

                UpdateBoardAfterSuccess();
                matching();
              
            }
            else
            {
                not_matching();
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


    public void nextQuestion()
    {

        Debug.Log("Scene Name " + SceneManager.GetActiveScene().name + "number " + SceneManager.GetActiveScene().buildIndex);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 3)
        {
            //it was first. 
            currentScene = 39;
        }
        else
        {
            currentScene++;
        }
        if (currentScene <= 48)
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
