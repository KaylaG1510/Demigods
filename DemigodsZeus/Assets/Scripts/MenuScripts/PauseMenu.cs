using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject thisButton;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    //User clicks pause button, pause game and display menu
    public void OnPause()
    {
        Debug.Log("Pause button pressed :D");
        //hide pause button whilst paused
        pauseMenu.SetActive(true);
        thisButton.SetActive(false);
        //pause/freeze any animated things
        Time.timeScale = 0f;
    }

    //user clicks resume button, return to game
    public void OnResume()
    {
        Debug.Log("Resume activated");
        //disable pause menu and reactivate pause button
        pauseMenu.SetActive(false);
        thisButton.SetActive(true);
        //Resume moving things
        Time.timeScale = 1f;
    }

    //user clicks restart button, restart scene
    public void OnRestart()
    {
        Debug.Log("Restart button clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //to fix freezing bug
        OnResume();
    }

    //user clicks return to main menu button, load MainMenu scene (0)
    public void OnMainMenu()
    {
        Debug.Log("Return to main menu button clicked");
        SceneManager.LoadScene("MainMenu");
    }

    //user clicks exit game button, closes game application
    public void OnExit()
    {
        Debug.Log("Exit application button clicked");
        Application.Quit();
    }
}
