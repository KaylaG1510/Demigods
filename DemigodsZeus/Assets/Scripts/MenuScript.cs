using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    //Menu States
    public enum MenuStates { Main, Instructions, CharacterSelection, Options };

    //each menu option from main menu
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
        switchMenu(MenuStates.Main);
    }

    //When New Game button is pressed
    public void OnNewGame()
    {
        Debug.Log("On New Game");
        //starts game scene (level1?)
    }

    //When Instructions button is pressed
    public void OnInstructions()
    {
        Debug.Log("You have entered Instructions Menu");
        switchMenu(MenuStates.Instructions);
    }

    //When Character Selection button is pressed
    public void OnCharacterSelection()
    {
        Debug.Log("On character selection");
        switchMenu(MenuStates.CharacterSelection);
    }

    //When Options button is pressed
    public void OnOptions()
    {
        Debug.Log("On options");
        switchMenu(MenuStates.Options);
    }

    //When Exit button is pressed
    public void OnExit()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    /**
     * 
     */
    private void switchMenu(MenuStates menu)
    {
        GameObject newState;

        switch (menu)
        {
            case MenuStates.Main:
                newState = mainMenu;
                break;
            case MenuStates.Instructions:
                newState = instructionsMenu;
                break;
            case MenuStates.CharacterSelection:
                newState = characterMenu;
                break;
            case MenuStates.Options:
                newState = optionsMenu;
                break;
            default:
                newState = mainMenu;
                break;
        }
        //stops text from staying orange
        currentState.GetComponentInChildren<Text>().color = Color.white;
        currentState.SetActive(false);
        //currentState.GetComponent<CanvasGroup>().interactable = false;
        currentState = newState;
        currentState.SetActive(true);
        //currentState.GetComponent<CanvasGroup>().interactable = true;
    }
}
