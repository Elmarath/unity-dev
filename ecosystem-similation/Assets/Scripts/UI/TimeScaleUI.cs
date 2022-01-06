using UnityEngine;
using UnityEngine.UI;

public class TimeScaleUI : MonoBehaviour
{
    public GameObject gameManagerRef;
    private GameManager gameManager;
    private float timeScale;
    private Text curText;

    // Start is called before the first frame update
    void Start()
    {
        curText = GetComponent<Text>();
        gameManager = gameManagerRef.GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        timeScale = gameManager.timeScale;
        int _timeScale = Mathf.RoundToInt(timeScale);
        SetText("Time Scale: x" + _timeScale.ToString());
    }
    private void SetText(string text)
    {
        curText.text = text;
    }

}
