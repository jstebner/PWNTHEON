using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attackMeter : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void setMaxAttackMeter (int max) {
        slider.maxValue = max;
        slider.value = max;
    }
    public void setAttackMeter(int attackMeterValue) {
        slider.value = attackMeterValue;
    }
}
