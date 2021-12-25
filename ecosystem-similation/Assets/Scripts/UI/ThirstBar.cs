using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    private Slider slider;
    private float _maxThirst;

    private void Awake()
    {
        slider = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
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