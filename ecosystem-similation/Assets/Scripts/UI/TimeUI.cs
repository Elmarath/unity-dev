using UnityEngine;
using UnityEngine.UI;


public class TimeUI : MonoBehaviour
{
    private Text curText;

    // Start is called before the first frame update
    void Start()
    {
        curText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int _timeScale = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        SetText("Time: " + _timeScale.ToString() + "s");
    }

    private void SetText(string text)
    {
        curText.text = text;
    }
}
