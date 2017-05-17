using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Gamehandelerr : MonoBehaviour {

	   public EventSystem mySystem;
    public List<Selectable> selectables;
    private GameObject lastSelectedObject;
    static int score = 0;
    public Text scoreText;



    void Start()
    {


        selectables = Selectable.allSelectables;
     

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

        score = score + 9;
        score++;
        scoreText.text = "Score: " + score;
    }

}

