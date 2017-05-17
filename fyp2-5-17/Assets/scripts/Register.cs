using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{


    private string connectionString;



    public InputField username;

    public InputField password;





    private string Username;
    private string Password;

    //   // int roll
    //    int member;
    //    int pass;





    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";


    
      



    }



    public void RegisterButton()
    {

        Insertstudent(Username,Password);
        print("Registration sucessful");
        SceneManager.LoadScene(10);

    }





    public void Insertstudent(string Username, string Password)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();


            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("INSERT INTO member (username,password)VALUES(\'{0}\',\'{1}\')",Username, Password);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
            }
            dbConnection.Close();

        }



    }







    //    // Update is called once per frame
    void Update()
            {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }


        }

        Username = username.GetComponent<InputField>().text;
        Password= password.GetComponent<InputField>().text;


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (username != null && password != null)

            {
                RegisterButton();
            }
        }


    }
    }
