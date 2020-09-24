using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        loseMenu.SetActive(false);
    }

    public void GameIsOver()
    {
        Time.timeScale = 0;
        loseMenu.SetActive(true);
    }
}
