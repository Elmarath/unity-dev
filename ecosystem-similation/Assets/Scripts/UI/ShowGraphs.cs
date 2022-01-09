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
    public void ShowWaitTime()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.waitTime);
        }
        PrintToGraph(valueList);
    }
    public void ShowMinSearchDistance()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.minSearchDistance);
        }
        PrintToGraph(valueList);
    }
    public void ShowMaxHunger()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.maxHunger);
        }
        PrintToGraph(valueList);
    }
    public void ShowMaxThirst()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.maxThirst);
        }
        PrintToGraph(valueList);
    }
    public void ShowMaxReproduceUrge()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.maxReproduceUrge);
        }
        PrintToGraph(valueList);
    }
    public void ShowHowManyChildren()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.howManyChildren);
        }
        PrintToGraph(valueList);
    }
    public void ShowGettingHungryRate()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.gettingHungryRate);
        }
        PrintToGraph(valueList);
    }
    public void ShowGettingThirstyRate()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.gettingThirstyRate);
        }
        PrintToGraph(valueList);
    }
    public void ShowGettingHornyRate()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.gettingHornyRate);
        }
        PrintToGraph(valueList);
    }
    public void ShowPregnantTimeRate()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.pregnantTimeRate);
        }
        PrintToGraph(valueList);
    }
    public void ShpwGettingFullMultiplier()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.gettingFullMultiplier);
        }
        PrintToGraph(valueList);
    }
    public void ShowDrinkingRate()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.drinkingRate);
        }
        PrintToGraph(valueList);
    }
    public void ShowBecomeAdultTime()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.becomeAdultTime);
        }
        PrintToGraph(valueList);
    }
    public void ShowBornAfterSec()
    {
        foreach (AnimalAttributes animal in gameManager.animalAttributesInMinute)
        {
            valueList.Add(animal.bornAfterSec);
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
