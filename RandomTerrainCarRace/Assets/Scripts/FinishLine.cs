using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public Color[] winnerColors;

    private void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Car 1")
        {
            Debug.Log("Blue Wins");
            gameManager.UpdateWinnerScreen(winnerColors[0], "Blue Wins!");
        }
        else if(other.transform.name == "Car 2")
        {
            Debug.Log("Red Wins");
            gameManager.UpdateWinnerScreen(winnerColors[1], "Red Wins!");
        }
    }
}
