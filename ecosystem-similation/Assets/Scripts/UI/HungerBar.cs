using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetMaxHunger(float maxHunger)
    {

        slider.maxValue = maxHunger;
        slider.value = maxHunger;
    }

    public void SetHealth(float hunger)
    {
        slider.value = hunger;
    }
}