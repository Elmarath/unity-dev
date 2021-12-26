using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    private float _maxThirst;

    private Slider slider;

    public void _init_(Slider _slider)
    {
        slider = _slider;
    }

    public void SetMaxThirst(float maxThirst)
    {
        _maxThirst = maxThirst;
        slider.maxValue = maxThirst;
        slider.value = 0;
    }

    public void SetThirst(float thirst)
    {
        slider.value = _maxThirst - thirst;
    }
}