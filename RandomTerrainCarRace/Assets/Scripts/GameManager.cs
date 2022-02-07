using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject DistanceCounter;
    public GameObject WinnerScreen;

    public Color[] winnerColors;

    private Color winnerColor;
    private CarController car1Controller;
    private CarController2 car2Controller;
    private Text winnerText;
    private Text counterText;

    private bool isWinnerDecided = false;
    private string whoWinsText;

    void Start()
    {
        car1Controller = car1.GetComponent<CarController>();
        car2Controller = car2.GetComponent<CarController2>();
        counterText = DistanceCounter.GetComponent<Text>();
        winnerText = WinnerScreen.GetComponent<Text>();

        WinnerScreen.SetActive(false);

        //WinnerScreen.SetActive(false);

        Debug.Log(car1);
        Debug.Log(car2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWinner();
        UpdateTextCounterText();
    }

    void CheckForWinner()
    {
        if (!isWinnerDecided)
        {
            int distanceZ = (int)(Mathf.Abs(car1.transform.position.z - car2.transform.position.z) * 2.4);
            if (distanceZ >= 100)
            {
                if (car1.transform.position.z > car2.transform.position.z)
                {
                    Debug.Log("Blue Wins!");
                    whoWinsText = "BLUE WINS!";
                    winnerColor = winnerColors[0];
                }
                else
                {
                    Debug.Log("Red Wins!");
                    whoWinsText = "RED WINS!";
                    winnerColor = winnerColors[1];
                }
                UpdateWinnerScreen(winnerColor, whoWinsText);
            }
        }
    }

    void UpdateTextCounterText()
    {
        counterText = DistanceCounter.GetComponent<Text>();
        int distanceZ = (int)(Mathf.Abs(car1.transform.position.z - car2.transform.position.z) * 2.4);
        counterText.text = distanceZ.ToString();
    }

    public void UpdateWinnerScreen(Color _winnerColor, string _whoWinsText)
    {
        if(!isWinnerDecided)
        {
            WinnerScreen.SetActive(true);
            winnerText.color = _winnerColor;
            winnerText.text = _whoWinsText;
            isWinnerDecided = true;
        }
    }

    public void ReStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
