using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /**
     * Holds a static reference to itslef to make sure there is only ever one
     * GameManager in existence, 'Singleton'.
     */
    static GameManager current;

    public float deathSequenceDuration = 1.5f;      //Player death duration before restarting scene

    bool isGameOver;

    //called when the script instance is loaded
    private void Awake()
    {
        //If a game manager exists in another instance...
        if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        //set this as the current game manager
        current = this;

        //Dont destroy manager between loading scenes
        DontDestroyOnLoad(gameObject);
    }

    //called once per frame
    private void Update()
    {
        if (isGameOver)
            return;
    }

    public static bool IsGameOver()
    {
        //no game manager; return false
        if (current == null)
            return false;

        //return game state
        return current.isGameOver;
    }

    public static void PlayerDied()
    {
        if (current == null)
            return;

        //Invoke the RestartScene() method after small delay
        current.Invoke("RestartScene", current.deathSequenceDuration);
    }

    public void PlayerWon()
    {
        if (current == null)
            return;

        //player won, game is now over
        current.isGameOver = true;

        //Get UIManager to display Game Over
        UIManager.DisplayGameOver();
        //AudioManager to play Game Over music
        //AudioManager.PlayGameWonAudio();
    }

    void RestartScene()
    {
        //if we have have a restart scene sound
        //AudioManager.PlayRestartSceneAudio();

        //Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
