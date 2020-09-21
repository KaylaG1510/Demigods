using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    public void OnEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
