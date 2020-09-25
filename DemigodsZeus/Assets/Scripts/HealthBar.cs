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
}
