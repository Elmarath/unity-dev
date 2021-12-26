using UnityEngine;
using UnityEngine.UI;

public class CurrentStateTextUI : MonoBehaviour
{

    private Text text1;

    public void _init_(Text _text)
    {
        text1 = _text;
    }

    public void SetStateText(string text)
    {
        text1.text = text;
    }
}