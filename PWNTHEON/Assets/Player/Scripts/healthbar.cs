using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setMaxHealth (float max) {
        slider.maxValue = max;
        slider.value = max;
        fill.color = gradient.Evaluate(1f);
    }
    public void setHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public float getHeath(){
        return slider.value;
    }
    public float getMaxHeath(){
        return slider.maxValue;
    }

}
