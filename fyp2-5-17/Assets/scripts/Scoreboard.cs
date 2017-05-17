using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;


public class Scoreboard : MonoBehaviour
{
    
    public GameObject ScorePrefeb;
    public Text enterName;
    public GameObject nameDialog;
    public Dropdown Dropdown;
    private string connectionString;
  
    ArrayList scores_list;


    public Transform scoreTrasnform;


    void Start()
    {
     
     
        connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";

      


    }

    void update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    nameDialog.SetActive(!nameDialog.active);

        //}

    }


    private ArrayList GetHighScoresList(int target_id, bool isGame)
    {
        

        Debug.Log("Func");
        ArrayList scores = new ArrayList();

        //Debug.Log(Name + " and score" + Score);
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            Debug.Log("DB");


            using (IDbCommand dbCmd = dbConnection.CreateCommand())
           {
                Debug.Log("read");

                string sqlQuery = ("select u.username, s.score, g.game_name,  u.user_id, s.game_id from score s inner join member as u on s.user_id = u.user_id and s.user_id = " + target_id + " inner join game as g on g.game_id = s.game_id order by s.score desc");
                if (isGame) sqlQuery = "select u.username,s.score, g.game_name, u.user_id, s.game_id from score s inner join member as u on s.user_id = u.user_id and s.game_id = "+ target_id +" inner join game as g on  g.game_id = " + target_id + " order by s.score desc";
                Debug.Log(sqlQuery);
                dbCmd.CommandText = sqlQuery;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    Debug.Log("Found soemthing");
                    while (reader.Read())
                    {
                        Debug.Log("Found soemthing in loop");
                        //db ka data object mein  fill kia ha
                    
                        HighScore obj = new HighScore();
                        obj.Username = reader.GetString(0);
                        obj.Score = reader.GetInt32(1);
                        obj.Gamename = reader.GetString(2);

                        scores.Add(obj);
                        //Debug.Log("First: " + reader.GetString(0));
                        //Debug.Log("Second: " + reader.GetInt32(1));
                        //Debug.Log("Third: " + reader.GetInt32(2));
                        //Debug.Log("Fourth: " + reader.GetInt32(3));
                    }
                    Debug.Log("\n");

                }
            }
            dbConnection.Close();
        }

        return scores;

    }

    
    public void showScore()

    {
        foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score"))

        {
            Debug.Log("in loop");

           Destroy(score);

        }
       

        bool isGame = this.Dropdown.value == 0 ? false : true;
        int target_id = this.GetSearchedObjectId(isGame);
        if (target_id > 0)
        {
            this.scores_list = this.GetHighScoresList(target_id, isGame);
   
            int i = 1;

            for (i = 0; i < this.scores_list.Count; i++)
            {
                
                HighScore score = this.scores_list[i] as HighScore;



                GameObject tempobjec = Instantiate(ScorePrefeb);

                Debug.Log("Obj1" + tempobjec.GetComponent<Highscorescript>());
                    

                tempobjec.GetComponent<Highscorescript>().SetScores(score.Username, score.Gamename, score.Score);
                tempobjec.transform.SetParent(scoreTrasnform);
                

            }

        }
        else
        {
            //show alert here that no user has been entered for search
        }    
        
    }
    
    public int GetSearchedObjectId(bool isGame)
    {
  
        


        if (enterName.text != string.Empty)
        {

            if (ScorePrefeb != null) Destroy(ScorePrefeb);
            Debug.Log("Func");
           

            //Debug.Log(Name + " and score" + Score);
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();
                Debug.Log("DB");


                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    Debug.Log("read");

                    string sqlQuery = ("select user_id from member where Upper(username) = '" + enterName.text.ToUpper() + "'");
                    if (isGame) sqlQuery = ("select game_id from game where Upper(game_name) = '" + enterName.text.ToUpper() + "'");
                    Debug.Log(sqlQuery);
                    dbCmd.CommandText = sqlQuery;                                                                           
                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        Debug.Log("Executed Statment");
                        while (reader.Read())
                        {
                            Debug.Log("Found soemthing in loop");
                            int userId = reader.GetInt32(0);
                            dbConnection.Close();

                            return userId;
                        }
                        

                    }
                }
                dbConnection.Close();
            }
        }
        return 0;
    }

}
