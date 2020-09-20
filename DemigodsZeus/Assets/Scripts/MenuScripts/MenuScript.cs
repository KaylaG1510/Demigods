using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Menu States
    public enum MenuStates { Main, Instructions, CharacterSelection, Options };

    //each menu option from main menu, set from Inspector
    public GameObject mainMenu;
    public GameObject instructionsMenu;
    public GameObject characterMenu;
    public GameObject optionsMenu;

    //current menu state
    public GameObject currentState;

    //On Main Menu
    public void OnMainMenu()
    {
        Debug.Log("on main menu!");
        //change to main menu
        switchMenu(MenuStates.Main);
    }

    //When New Game button is pressed
    public void OnNewGame()
    {
        Debug.Log("On New Game");
        //starts game scene (level1)
        //will load new level scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); //Scene 0 = Main Menu, Scene 1 = Level One
        Time.timeScale = 1;
    }

    //When Instructions button is pressed
    public void OnInstructions()
    {
        Debug.Log("You have entered Instructions Menu");
        //change to instructions menu
        switchMenu(MenuStates.Instructions);
    }

    //When Character Selection button is pressed
    public void OnCharacterSelection()
    {
        Debug.Log("On character selection");
        //change to character selection menu
        switchMenu(MenuStates.CharacterSelection);
    }

    //When Options button is pressed
    public void OnOptions()
    {
        Debug.Log("On options");
        //change to options menu
        switchMenu(MenuStates.Options);
    }

    //When Exit button is pressed
    public void OnExit()
    {
        Debug.Log("Exit game");
        //Closes games completely, unless in play mode
        Application.Quit();
    }

    //When Fullscreen mode button pressed
    public void OnFullScreen()
    {
        Debug.Log("Entering fullscreen!");
        //set screen to fullscreen
        Screen.fullScreen = true;
    }

    //When Windowed button is pressed
    public void OnWindowed()
    {
        Debug.Log("Entering Windowed mode!");
        //exit fullscreen mode
        Screen.fullScreen = false;
    }

    /**
     * Changes displayed menu to new menu from
     * corresponding button click.
     */
    private void switchMenu(MenuStates menu)
    {
        GameObject newState;

        //Change different menu options
        switch (menu)
        {
            //Main Menu
            case MenuStates.Main:
                newState = mainMenu;
                break;
            //Instructions Menu
            case MenuStates.Instructions:
                newState = instructionsMenu;
                break;
            //Character Selection Menu
            case MenuStates.CharacterSelection:
                newState = characterMenu;
                break;
            //Options Menu
            case MenuStates.Options:
                newState = optionsMenu;
                break;
            //Default Menu is main menu
            default:
                newState = mainMenu;
                break;
        }
        //stops text from staying orange
        currentState.GetComponentInChildren<Text>().color = Color.white;
        //make current menu no longer active
        currentState.SetActive(false);
        //set new current menu to user choice via corresponding buttons
        currentState = newState;
        //reactivate current menu as new menu
        currentState.SetActive(true);
    }
}
