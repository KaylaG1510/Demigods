using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject winMenu;  //menu displays when player wins

    private void Start()
    {
        winMenu.SetActive(false);   //starts deactivated
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

    //Return to main menu (okay button pressed) SPRINT ONE ONLY
    public void OnEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
