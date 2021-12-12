using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Slider slider;
    private Transform hungerBar;

    private void Awake()
    {
        slider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
    }

    public void SetMaxHunger(float maxHunger)
    {

        slider.maxValue = maxHunger;
        slider.value = maxHunger;
    }

    public void SetThirst(float hunger)
    {
        slider.value = hunger;
    }
}