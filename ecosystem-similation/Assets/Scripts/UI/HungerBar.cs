using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private float _maxHunger;
    private Slider slider;

    public void _init_(Slider _slider)
    {
        slider = _slider;
    }

    public void SetMaxHunger(float maxHunger)
    {
        _maxHunger = maxHunger;
        slider.maxValue = maxHunger;
        slider.value = 0;
    }

    public void SetThirst(float hunger)
    {
        slider.value = hunger;
    }
}