using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class PauseTest : UITest
{
    /**
     * Does the pause menu pull up correctly from button 
     */
    [UnityTest]
    public IEnumerator pauseMenuDisplays()
    {
        yield return LoadScene("LevelOne");

        yield return new WaitForSecondsRealtime(2);

        yield return Press("PauseButton");

        yield return WaitFor(new ObjectAppeared("PauseMenu"));
    }

    /**
     * Does the gameplay freeze when game is paused?
     */
    [UnityTest]
    public IEnumerator gameplayFreezes()
    {
        yield return LoadScene("LevelOne");

        yield return Press("PauseButton");

        yield return WaitFor(new ObjectAppeared("PauseMenu"));

        //yield return WaitFor(new BoolCondition(() => Time.timeScale == 0));


        GameObject playerObject = GameObject.FindGameObjectWithTag("Player").gameObject;
        //this.animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName");
        //playerObject = GameObject.Instantiate(Resources.Load("Prefabs/Hero")) as GameObject;

        Animator currentAnim = playerObject.GetComponent<Animator>();

        yield return WaitFor(new BoolCondition(() => currentAnim.updateMode == AnimatorUpdateMode.Normal));
    }

    /**
     * Checks the scene restarts properly from the pause menu and restart button
     */
    [UnityTest]
    public IEnumerator sceneRestarts()
    {
        yield return pauseMenuDisplays();

        yield return Press("RestartButton");

        yield return WaitFor(new ObjectDisappeared("PauseMenu"));

        //yield return WaitFor(new ObjectDisappeared("Hero"));

        yield return WaitFor(new ObjectAppeared("Hero"));
    }

    /** Makes sure when on pause menu, return to main menu button
     * loads the main menu scene correctly*/
    [UnityTest]
    public IEnumerator ReturnToMainMenuScene()
    {
        yield return LoadScene("LevelOne");

        yield return pauseMenuDisplays();

        yield return Press("ReturnMenuButton");

        yield return WaitFor(new ObjectAppeared("MainMenu"));
    }
}
