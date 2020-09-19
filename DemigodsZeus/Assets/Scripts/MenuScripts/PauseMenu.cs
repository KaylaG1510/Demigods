using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public enum GameStates { PLAY, PAUSE, DEAD, FIN };

    public GameObject pauseMenu;
    public GameObject thisButton;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    //User clicks pause button
    public void OnPause()
    {
        Debug.Log("Pause button pressed :D");
        pauseMenu.SetActive(true);
        thisButton.SetActive(false);
    }

    public void OnResume()
    {
        Debug.Log("Resume activated");
        pauseMenu.SetActive(false);
        thisButton.SetActive(true);
    }


}
