using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerchange : MonoBehaviour {
    public string changeTheScene;
    private float timer = 10f;
    //private Text timerSeconds;

	// Use this for initialization
	void Start () {

       // timerSeconds = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
        //timer functionality
         timer -= Time.deltaTime;

        if (timer <= 0 )
        {
            
            SceneManager.LoadScene(9);
        }

    }
}
