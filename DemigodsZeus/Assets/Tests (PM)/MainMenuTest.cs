using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using UnityEngine.UI;

public class MainMenuTest : UITest
{
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
        //Wait 3 more seconds
        yield return new WaitForSecondsRealtime(3);
        //return to main menu by pressing back button
        yield return Press("BackButton");
        yield return new WaitForSecondsRealtime(1);
    }

    [UnityTest]
    public IEnumerator OptionsWindowedMode()
    {
        yield return LoadScene("MainMenu");

        yield return Press("OptionsButton");

        yield return WaitFor(new ObjectAppeared("OptionsMenu"));

        yield return Press("WindowedButton");

        yield return WaitFor(new BoolCondition(() => !Screen.fullScreen)); 
    }

    [UnityTest]
    public IEnumerator OptionsFullscreenMode()
    {
        yield return LoadScene("MainMenu");

        yield return Press("OptionsButton");

        yield return WaitFor(new ObjectAppeared("OptionsMenu"));

        yield return Press("FullscreenButton");

        yield return WaitFor(new BoolCondition(() => Screen.fullScreen));
    }

    [UnityTest]
    public IEnumerator NewGameButtonLoadsLevel()
    {
        yield return LoadScene("MainMenu");

        yield return Press("NewGameButton");

        yield return WaitFor(new ObjectAppeared("Level1"));

        yield return new WaitForSecondsRealtime(3);
    }
}

