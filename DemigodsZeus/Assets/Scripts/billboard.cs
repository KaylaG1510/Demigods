using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Transform cam;
    public GameObject healthBar;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    private void Start()
    {
        healthBar = GameObject.Find("Health bar");
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            healthBar.SetActive(true);
        }

        if(Time.timeScale == 0)
        {
            healthBar.SetActive(false);
        }
    }
}
