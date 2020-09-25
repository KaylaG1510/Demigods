using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject loseMenu; //panel that displays when player loses
    [SerializeField] private float delayInterval = 2.5f; //default value


    // Start is called before the first frame update
    void Start()
    {
        //lose menu deactivated initially
        loseMenu.SetActive(false);
    }

    //Invoked aftera a delay when game is over
    public void GameIsOver()
    {
        Debug.Log("Invoke called correctly");
        //freeze gameplay
        Time.timeScale = 0;
        //activate lose menu
        loseMenu.SetActive(true);
    }

    //Provide a small delay to gameover so Death animation has time
    //to happen
    public void InvokeMenuAfterDeath()
    {
        Debug.Log("Invoke called");
        Invoke("GameIsOver", 2.0f); //2 second delay
    }
}
