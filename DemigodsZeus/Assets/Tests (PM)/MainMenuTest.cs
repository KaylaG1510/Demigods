using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class MainMenuTest
{
    private bool clicked;

    [UnityTest]
    public IEnumerator InstructionsButtonWorks()
    {
        //GameObject InstructionMenuGameObject = GameObject.Find("InstructionsMenu");
        //GameObject MainMenuGameObject = GameObject.Find("MainMenu");

        //GameObject Canvas = GameObject.Find("Canvas");
        //GameObject menu = Canvas.transform.GetChild(0).gameObject;
        //var menupanel = menu.GetComponent<>

        //Assert.True(menu.activeInHierarchy);

        //MenuScript script = InstructionMenuGameObject.AddComponent<MenuScript>();

        //script.OnInstructions();

        //Checks instructions menu is ative panel when instructions button is clicked
        //and main menu is disabled (only way to call onInstructions is through main menu
        //Assert.IsTrue(InstructionMenuGameObject.activeSelf);

        yield return null;

        //GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas").gameObject);
        //Assert.NotNull(gameGameObject);
        //var canvasGameObject = GameObject.FindGameObjectWithTag("test");
        //Assert.NotNull(canvasGameObject);
        //var MainMenuUI = canvasGameObject.transform.Find("MainMenu");
        GameObject MainMenuUI = GameObject.Find("MainMenu").gameObject;
        Assert.NotNull(MainMenuUI);
        //var instructionsButton = GameObject.Find("InstructionsButton");
        //var instructionsButtonGO = MainMenuUI.transform.Find("InstructionsButton");
        //var mainTransform = MainMenuUI.transform;
        //Assert.NotNull(mainTransform);
        GameObject instructionsButtonGO = MainMenuUI.transform.Find("InstructionsButton").gameObject;
        Assert.NotNull(instructionsButtonGO);
        var instructionsButton = instructionsButtonGO.GetComponent<Button>();

        Assert.NotNull(instructionsButton);
        clicked = false;

        instructionsButton.onClick.AddListener(Clicked);
        instructionsButton.onClick.Invoke();

        Assert.True(clicked);
        yield return null;
        yield return new WaitForSeconds(1);

        //MainMenuUI = canvasGameObject.transform.Find("InstructionsMenu");
        MainMenuUI = GameObject.Find("InstructionsMenu");
        Assert.NotNull(MainMenuUI);
        Transform MainMenuUI_Transform = MainMenuUI.transform;

        Assert.NotNull(MainMenuUI_Transform);
        Assert.True(MainMenuUI.gameObject.activeSelf);
    }

    private void Clicked()
    {
        clicked = true;
    }
}

