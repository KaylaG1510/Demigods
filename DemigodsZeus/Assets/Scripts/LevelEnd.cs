using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject winMenu;  //menu displays when player wins
    public GameObject loseMenu; //menu displays when game over

    private void Start()
    {
        winMenu.SetActive(false);   //starts deactivated
        loseMenu.SetActive(false);
    }

    //When player collides with object which triggers end of level
    //attachable to gameobject or canvas
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //freeze gameplay
            Time.timeScale = 0;
            //activate win menu
            winMenu.SetActive(true);
        }
    }

    public void OnEnd()
    {
        string s_name = SceneManager.GetActiveScene().name;
        if (s_name.CompareTo("LevelThree") == 0)
        {
            //**ADD DELAY???
            SceneManager.LoadScene("EndCredit");
        }
        else if (s_name.CompareTo("LevelTwo") == 0)
        {
            SceneManager.LoadScene("LevelThree");
            Time.timeScale = 1f;
        }
        else if (s_name.CompareTo("LevelOne") == 0)
        {
            SceneManager.LoadScene("LevelTwo");
            Time.timeScale = 1f;
        }

    }

    //Restart Level, restart button pressed
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        loseMenu.SetActive(false);
        //Resume deltaTime animations
        Time.timeScale = 1f;
    }

    //Exit game completely, exit button pressed
}
