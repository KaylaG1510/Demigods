using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;   //health bar

    //sets health to maximum value
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //sets health to new health value
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            //gameObject.SetActive(false);
            transform.position = new Vector2(transform.position.x - 20000, transform.position.y + 10000);
        }

        //if (Time.timeScale == 1)
        //{
        //    transform.position = new Vector2(transform.position.x + 20000, transform.position.y - 10000);
        //}
    }
}
