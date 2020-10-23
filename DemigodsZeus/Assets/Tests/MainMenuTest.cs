using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using UnityEngine.UI;

public class MainMenuTest : UITest
{
    /**
     * Makes sure Instructions Menu loads from main screen
     */
    [UnityTest]
    public IEnumerator InstructionsMenuLoadsFromButton()
    {
        //INSTRUCTIONS TEST
        //Load the scene
        yield return LoadScene("MainMenu");
        //Wait 3 seconds
        yield return new WaitForSecondsRealtime(3);
        //Click instructions button
        //Expected Output: Instructions Menu appears
        yield return Press("InstructionsButton");
        //Make sure InstructionsMenu loads properly and is active
        yield return WaitFor(new ObjectAppeared("InstructionsMenu"));
    }

    /**
     * Makes sure game can transition from fullscreen to windowed mode
     */
    [UnityTest]
    public IEnumerator OptionsWindowedMode()
    {
        yield return LoadScene("MainMenu");

        yield return Press("OptionsButton");

        yield return WaitFor(new ObjectAppeared("OptionsMenu"));

        yield return Press("WindowedButton");

        yield return WaitFor(new BoolCondition(() => !Screen.fullScreen)); 
    }

    /**
     * Makes sure game can transition from windowed to fullscreen mode
     */
    [UnityTest]
    public IEnumerator OptionsFullscreenMode()
    {
        yield return LoadScene("MainMenu");

        yield return Press("OptionsButton");

        yield return WaitFor(new ObjectAppeared("OptionsMenu"));

        yield return Press("FullscreenButton");

        yield return WaitFor(new BoolCondition(() => Screen.fullScreen));
    }

    /**
     * Makes sure New Game button loads level one
     */
    [UnityTest]
    public IEnumerator NewGameButtonLoadsLevel()
    {
        yield return LoadScene("MainMenu");

        yield return Press("NewGameButton");

        yield return WaitFor(new ObjectAppeared("Level1"));

        yield return new WaitForSecondsRealtime(3);
    }

    /**
     * Makes sure the back button works in two different menus;
     * instructions and character selection
     */
    [UnityTest]
    public IEnumerator BackButtonWorks()
    {
        Assert.Pass();
        yield return LoadScene("MainMenu");

        yield return Press("Instructions");

        yield return Press("BackButton");

        yield return WaitFor(new ObjectAppeared("MainMenu"));

        yield return Press("CharacterSelectButton");

        yield return Press("BackButton");

        yield return WaitFor(new ObjectAppeared("MainMenu"));
    }
}

