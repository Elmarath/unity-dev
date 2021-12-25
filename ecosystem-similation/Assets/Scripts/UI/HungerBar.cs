using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Slider slider;
    private float _maxHunger;

    private void Awake()
    {
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }

    public void SetMaxHunger(float maxHunger)
    {
        _maxHunger = maxHunger;
        slider.maxValue = maxHunger;
        slider.value = 0;
    }

    public void SetThirst(float hunger)
    {
        slider.value = _maxHunger - hunger;
    }
}