using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject loseMenu;
    [SerializeField] private float delayInterval = 2.5f; //default value
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        loseMenu.SetActive(false);
    }

    private void Update()
    {
        //timeElapsed += Time.deltaTime;
    }

    public void GameIsOver()
    {
        Debug.Log("Invoke called correctly");
        Time.timeScale = 0;
        loseMenu.SetActive(true);
    }

    public void InvokeMenuAfterDeath()
    {
        Debug.Log("Invoke called");
        Invoke("GameIsOver", 2.0f);
    }
}
