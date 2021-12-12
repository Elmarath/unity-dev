using UnityEngine;
using UnityEngine.UI;

public class ThirstBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = transform.GetChild(1).GetChild(1).GetComponent<Slider>();
    }

    public void SetMaxThirst(float maxThirst)
    {
        slider.maxValue = maxThirst;
        slider.value = maxThirst;
    }

    public void SetThirst(float thirst)
    {
        slider.value = thirst;
    }
}