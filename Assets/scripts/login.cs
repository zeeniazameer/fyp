using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class login : MonoBehaviour
{


    private string connectionString;


    public InputField username;
    public InputField password;

    private string Username;
    private string Password;

    bool NA = false;
    bool SC = false;
 

    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";





    }






    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }



            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Username != "" && Password != "")
                {
                    LoginButton();

                }
            }



        }
    }

    public void LoginButton()
    {


        Getstudent();

    }

    public void Getstudent()

    {

        //Debug.Log(Name + " and score" + Score);
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        //Debug.Log(Name + " and score" + Score);
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();


            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = ("SELECT username ,password, user_id  FROM member where UPPER(username) = '" + Username.ToUpper() + "' and password = '" + Password + "'");

                Debug.Log(sqlQuery);
                dbCmd.CommandText = sqlQuery;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    //Debug.Log("Found soemthing");
                    bool success = false;

                    while (reader.Read())
                    {
                        //Debug.Log("Found soemthing in loop");

                       

                        Debug.Log("First: " + reader.GetString(0) + "Comparing with " + Username);
                        Debug.Log("Second: " + reader.GetString(1) + "Comparing with " + Password);
                        if (reader.GetString(0).ToUpper() == Username.ToUpper() && reader.GetString(1) == Password)
                        {
                            Debug.Log("Login Success" + reader.GetInt32(2));

                            if (reader.GetInt32(2) > 0)
                            {
                                Debug.Log("Login Success" + reader.GetInt32(2));

                                AppSettings.logged_in_user_id = reader.GetInt32(2);
                                success = true;
                            }
                        }

                    }
                    if (success)
                    {
                        Debug.Log("Login Success");
                        dbConnection.Close();
                        SceneManager.LoadScene(13);

                    }
                  
                    else
                    {
                        Debug.Log("Login Failed");

                    }
                }

            }
         dbConnection.Close();

        }
    }

}



