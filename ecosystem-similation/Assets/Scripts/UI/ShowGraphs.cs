using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGraphs : MonoBehaviour
{
    public GameObject windowGraphObj;

    private GameManager gameManager;
    private WindowGraph windowGraph;
    private List<float> valueList = new List<float>();
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        windowGraph = windowGraphObj.GetComponentInChildren<WindowGraph>();
    }

    public void ShowNormalSpeed()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.normalSpeed);
        }
        PrintToGraph(valueList);
    }

    private void PrintToGraph(List<float> list)
    {
        if (windowGraphObj.activeSelf)
        {
            windowGraphObj.SetActive(false);
        }
        else
        {
            windowGraphObj.SetActive(true);
            windowGraph.Init(list);
        }
    }
}
