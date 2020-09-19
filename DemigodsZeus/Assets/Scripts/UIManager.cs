using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Holds a static reference to itself to make sure only one UIManager is
    //in existence at any given time, using 'Singleton' design.
    static UIManager current;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    //called when the script instance is first loaded
    void Awake()
    {
        //UIManager exists other than this
        if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        //set current UIManager to stay between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);
        pauseMenu.SetActive(false);
    }

    public void DisplayGameOver()
    {
        //No UIManager exists
        if (current == null)
            return;

        //show the game over text
        //current.gameOverText.enabled = true;
    
    }

    public void DisplayPause()
    {
        if (current == null)
            return;
        //display pause menu
        pauseMenu.SetActive(true);
    }

    public void DisplayMainMenu()
    {
        if (current == null)
            return;
    }
}
