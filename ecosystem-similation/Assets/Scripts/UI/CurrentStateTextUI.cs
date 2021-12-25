using UnityEngine;
using UnityEngine.UI;

public class CurrentStateTextUI : MonoBehaviour
{
    private Text _text;
    private void Awake()
    {
        _text = transform.GetChild(0).GetChild(3).GetComponent<Text>();
    }

    public void SetStateText(string text)
    {
        _text.text = text;
    }
}