using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject winMenu;

    private void Start()
    {
        winMenu.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
        }
    }

    public void OnEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
