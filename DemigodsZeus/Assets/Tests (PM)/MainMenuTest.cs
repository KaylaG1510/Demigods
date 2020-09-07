using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MainMenuTest
{
    private MenuScript menu = new MenuScript();

    [UnityTest]
    public IEnumerator MainMenuStarts()
    {
        GameObject menuGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MainMenu"));

        menu = menuGameObject.GetComponent<MenuScript>();

        yield return null;
        //Tests Main Menu is active GameObject
        Assert.IsTrue(menuGameObject.activeInHierarchy);
        //needs to check instructions active and main menu not
    }
}

