using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dodgeMeter : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void setMaxHealth (int max) {
        slider.maxValue = max;
        slider.value = max;
    }
    public void setHealth(int dodgeMeterValue) {
        slider.value = dodgeMeterValue;
    }
}
