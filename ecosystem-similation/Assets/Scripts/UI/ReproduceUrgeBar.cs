using UnityEngine;
using UnityEngine.UI;

public class ReproduceUrgeBar : MonoBehaviour
{
    private float _maxReproduceUrge;

    private Slider slider;

    public void _init_(Slider _slider)
    {
        slider = _slider;
    }

    public void SetMaxReproduceUrge(float maxReproduceUrge)
    {
        _maxReproduceUrge = maxReproduceUrge;
        slider.maxValue = maxReproduceUrge;
        slider.value = 0;
    }

    public void SetReproduceUrge(float reproduceUrge)
    {
        slider.value = reproduceUrge;
    }
}