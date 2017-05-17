using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

    class DBManager
    {
        public static DBManager sharedManager = new DBManager();
        private string connectionString;

        DBManager()
        {
            connectionString = "URI=file:" + Application.dataPath + "/HighScoreDb.sqlite";

        }
        public bool InsertScore(int Score, int user_id, int game_id)
        {

            bool result = false;
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                Debug.Log(connectionString);


                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {

                    int score_id = 0, old_score = 0;
                    string getScoresQuery = ("select score_id, score from score where user_id = " + AppSettings.logged_in_user_id + " AND game_id = " + AppSettings.selected_game_id);
                    Debug.Log(getScoresQuery);
                    dbCmd.CommandText = getScoresQuery;
                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Debug.Log("Found Previous score");
                            old_score = reader.GetInt32(1);
                            score_id = reader.GetInt32(0);
                            break;
                        }
                    }
                    
                    


                    Debug.Log("score Id" + score_id);
                    Debug.Log("score" + old_score);

                    if (AppSettings.logged_in_user_id > 0 && AppSettings.selected_game_id > 0)
                    {
                        string sqlQuery = "";
                        if (score_id > 0)
                        {
                        //there is some previous score 
                        if (old_score < Score)
                        {
                            //update only if new score is greater
                            sqlQuery = String.Format("UPDATE score set Score = " + Score + " WHERE user_id = {0} AND game_id = {1}", AppSettings.logged_in_user_id, AppSettings.selected_game_id);
                        }
                        else
                        {
                            result = true;
                        }
                        }

                            else
                        {
                            sqlQuery = String.Format("INSERT INTO score(Score, user_id, game_id)VALUES({0},{1},{2})", Score, AppSettings.logged_in_user_id, AppSettings.selected_game_id);

                        }

                         if (sqlQuery.Length > 0)
                        {
                            Debug.Log("Saving Score with query " + sqlQuery);

                            dbCmd.CommandText = sqlQuery;
                            dbCmd.ExecuteScalar();
                            result = true;
                     
                    }
                    }
                    else
                    {

                        //show alert here that user is not logged in or no game is selected yet
                    }
                    dbConnection.Close();


                }


            }
        Debug.Log("Saved now returing from score with state " + result);

        return result;
        }

    }

