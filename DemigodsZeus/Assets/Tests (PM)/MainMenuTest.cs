using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MainMenuTest
{
    [UnityTest]
    public IEnumerator MainMenuStarts()
    {
        GameObject InstructionMenuGameObject = GameObject.Find("InstructionsMenu");
        GameObject MainMenuGameObject = GameObject.Find("MainMenu");

        //MenuScript script = new MenuScript();
        MainMenuGameObject.AddComponent<MenuScript>();

        MenuScript script = InstructionMenuGameObject.AddComponent<MenuScript>();

        script.OnInstructions();

        //Checks instructions menu is ative panel when instructions button is clicked
        //and main menu is disabled (only way to call onInstructions is through main menu
        Assert.IsTrue(InstructionMenuGameObject.activeSelf);

        yield return null;
    }
}

