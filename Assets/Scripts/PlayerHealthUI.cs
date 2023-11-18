using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillBar;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fillBar.color = gradient.Evaluate(1f);
    }

    public void SetHealthSlider(int health)
    {
        slider.value = health;

        fillBar.color = gradient.Evaluate(slider.normalizedValue);
    }

}
