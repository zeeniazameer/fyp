using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{

    public void ChangeToScene(int changeTheScene) {
        AppSettings.selected_game_id = changeTheScene;
        HighScore.user_onGoingtScores = 0;
        SceneManager.LoadScene(changeTheScene);

    }

}



