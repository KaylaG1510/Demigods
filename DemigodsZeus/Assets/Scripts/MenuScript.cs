using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //At game start, all canvas bar main menu should be disabled
    //On Main Menu
    public void OnMainMenu()
    {
        Debug.Log("on main menu!");
    }

    //When New Game button is pressed
    public void OnNewGame()
    {
        Debug.Log("On New Game");
    }

    //When Instructions button is pressed
    public void OnInstructions()
    {
        Debug.Log("You have entered Instructions Menu");

        //Change menu states
    }

    //When Character Selection button is pressed
    public void OnCharacterSelection()
    {
        Debug.Log("On character selection");
    }

    //When Options button is pressed
    public void OnOptions()
    {
        Debug.Log("On options");
    }

    //When Exit button is pressed
    public void OnExit()
    {
        Debug.Log("Exit game");
    }
}
