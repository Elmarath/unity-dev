using UnityEngine;
using UnityEngine.UI;

public class ReproduceUrgeBar : MonoBehaviour
{
    private Slider slider;
    private float _maxReproduceUrge;

    private void Awake()
    {
        slider = transform.GetChild(0).GetChild(2).GetComponent<Slider>();
    }

    public void SetMaxReproduceUrge(float maxReproduceUrge)
    {
        _maxReproduceUrge = maxReproduceUrge;
        slider.maxValue = maxReproduceUrge;
        slider.value = 0;
    }

    public void SetReproduceUrge(float reproduceUrge)
    {
        slider.value = _maxReproduceUrge - reproduceUrge;
    }
}