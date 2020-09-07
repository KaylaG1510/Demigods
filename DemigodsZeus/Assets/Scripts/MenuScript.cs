using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //Menu States
    public enum MenuStates { Main, Instructions, CharacterSelection, Options };

    public GameObject mainMenu;
    public GameObject instructionsMenu;
    public GameObject characterMenu;
    public GameObject optionsMenu;

    //current menu state
    //private GameObject currentState;
    public MenuStates currentState;

    //when script first starts
    private void Awake()
    {
        currentState = MenuStates.Main;
    }

    private void Update()
    {
        switch (currentState)
        {
            case MenuStates.Main:
                mainMenu.SetActive(true);
                instructionsMenu.SetActive(false);
                break;
            case MenuStates.Instructions:
                mainMenu.SetActive(false);
                instructionsMenu.SetActive(true);
                break;
            default:
                mainMenu.SetActive(true);
                instructionsMenu.SetActive(false);
                break;
        }
    }

    //On Main Menu
    public void OnMainMenu()
    {
        Debug.Log("on main menu!");
        //switchMenu(MenuStates.Main);
        currentState = MenuStates.Main;
        //currentState.SetActive(false);
        //currentState = mainMenu;
        //currentState.SetActive(true);
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
        //switchMenu(MenuStates.Instructions);
        currentState = MenuStates.Instructions;
        //currentState.SetActive(false);
        //currentState = mainMenu;
        //currentState.SetActive(true);
    }

    //When Character Selection button is pressed
    public void OnCharacterSelection()
    {
        Debug.Log("On character selection");
        //switchMenu(MenuStates.CharacterSelection);
    }

    //When Options button is pressed
    public void OnOptions()
    {
        Debug.Log("On options");
        //switchMenu(MenuStates.Options);
    }

    //When Exit button is pressed
    public void OnExit()
    {
        Debug.Log("Exit game");
    }

    /**
     * 
     */
    //private void switchMenu(MenuStates menu)
    //{
    //    GameObject newState;

    //    switch (menu)
    //    {
    //        case MenuStates.Main:
    //            newState = mainMenu;
    //            break;
    //        case MenuStates.Instructions:
    //            newState = instructionsMenu;
    //            break;
    //        case MenuStates.CharacterSelection:
    //            newState = characterMenu;
    //            break;
    //        case MenuStates.Options:
    //            newState = optionsMenu;
    //            break;
    //        default:
    //            newState = mainMenu;
    //            break;
    //    }

    //    currentState.SetActive(false);
    //    currentState = newState;
    //    currentState.SetActive(true);
    //}
}
