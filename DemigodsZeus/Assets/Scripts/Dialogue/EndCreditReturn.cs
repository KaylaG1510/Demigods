using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCreditReturn : MonoBehaviour
{
    public void toMainMenuScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public class PauseMenu : MonoBehaviour
    {

    }

}