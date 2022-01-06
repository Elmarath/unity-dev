using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AnimalAttributes avAnimalAttr = new AnimalAttributes();
    public float avarageNormalSpeed;
    public float timeScale;
    public GameObject initAnimals;
    public List<Animal> livingAdultAnimals = new List<Animal>();
    public List<AnimalAttributes> animalAttributesInMinute = new List<AnimalAttributes>();
    private int totalExistedAnimalsInMinute = 0;
    private float m_FixedDeltaTime;

    void Awake()
    {
        this.m_FixedDeltaTime = Time.fixedDeltaTime;
        this.timeScale = Time.timeScale;
        InvokeRepeating("ResetAnimalAttributesInMinute", 59f, 60f);
    }

    // Update is called once per frame
    void Update()
    {

        timeScale = Time.timeScale;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Time.timeScale >= 1f && Time.timeScale < 52f)
            {
                Time.timeScale += 3f;
            }
            else if (Time.timeScale >= 0.1 && Time.timeScale <= 1.1f)
            {
                Time.timeScale += 0.2f;
            }
            // The fixed delta time will now be 0.02 real-time seconds per frame
            Time.fixedDeltaTime = this.m_FixedDeltaTime * Time.timeScale;

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.timeScale >= 4f)
            {
                Time.timeScale -= 3f;
            }
            else if (Time.timeScale >= 0.3 && Time.timeScale <= 1.1f)
            {
                Time.timeScale -= 0.2f;
            }
            // The fixed delta time will now be 0.02 real-time seconds per frame
            Time.fixedDeltaTime = this.m_FixedDeltaTime * Time.timeScale;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Initializing new animals");
            Instantiate(initAnimals, new Vector3(0f, 0f, 0f), transform.rotation);
        }
    }

    public void AddToLivingAdultAnimals(Animal animal)
    {
        totalExistedAnimalsInMinute++;
        for (var i = livingAdultAnimals.Count - 1; i > -1; i--)
        {
            if (livingAdultAnimals[i] == null)
                livingAdultAnimals.RemoveAt(i);
        }
        livingAdultAnimals.Add(animal);
        livingAdultAnimals = livingAdultAnimals.Distinct().ToList();
        CalculateAvarageGenes(animal);
    }
    public void RemoveFromLivingAdultAnimals(Animal animal)
    {
        livingAdultAnimals.Remove(animal);
        for (var i = livingAdultAnimals.Count - 1; i > -1; i--)
        {
            if (livingAdultAnimals[i] == null)
                livingAdultAnimals.RemoveAt(i);
        }
        livingAdultAnimals = livingAdultAnimals.Distinct().ToList();
    }

    private void CalculateAvarageGenes(Animal animal)
    {
        avAnimalAttr.normalSpeed = CalculateNewAvarage(animal.normalSpeed, avAnimalAttr.normalSpeed, totalExistedAnimalsInMinute);
        avAnimalAttr.waitTime = CalculateNewAvarage(animal.waitTime, avAnimalAttr.waitTime, totalExistedAnimalsInMinute);
        avAnimalAttr.minSearchDistance = CalculateNewAvarage(animal.minSearchDistance, avAnimalAttr.minSearchDistance, totalExistedAnimalsInMinute);
        avAnimalAttr.maxHunger = CalculateNewAvarage(animal.maxHunger, avAnimalAttr.maxHunger, totalExistedAnimalsInMinute);
        avAnimalAttr.maxThirst = CalculateNewAvarage(animal.maxThirst, avAnimalAttr.maxThirst, totalExistedAnimalsInMinute);
        avAnimalAttr.maxReproduceUrge = CalculateNewAvarage(animal.maxReproduceUrge, avAnimalAttr.maxReproduceUrge, totalExistedAnimalsInMinute);
        avAnimalAttr.howManyChildren = CalculateNewAvarage(animal.howManyChildren, avAnimalAttr.howManyChildren, totalExistedAnimalsInMinute);
        avAnimalAttr.gettingHungryRate = CalculateNewAvarage(animal.gettingHungryRate, avAnimalAttr.gettingHungryRate, totalExistedAnimalsInMinute);
        avAnimalAttr.gettingThirstyRate = CalculateNewAvarage(animal.gettingThirstyRate, avAnimalAttr.gettingThirstyRate, totalExistedAnimalsInMinute);
        avAnimalAttr.gettingHornyRate = CalculateNewAvarage(animal.gettingHornyRate, avAnimalAttr.gettingHornyRate, totalExistedAnimalsInMinute);
        avAnimalAttr.pregnantTimeRate = CalculateNewAvarage(animal.pregnantTimeRate, avAnimalAttr.pregnantTimeRate, totalExistedAnimalsInMinute);
        avAnimalAttr.gettingFullMultiplier = CalculateNewAvarage(animal.gettingFullMultiplier, avAnimalAttr.gettingFullMultiplier, totalExistedAnimalsInMinute);
        avAnimalAttr.drinkingRate = CalculateNewAvarage(animal.drinkingRate, avAnimalAttr.drinkingRate, totalExistedAnimalsInMinute);
        avAnimalAttr.becomeAdultTime = CalculateNewAvarage(animal.becomeAdultTime, avAnimalAttr.becomeAdultTime, totalExistedAnimalsInMinute);
        avAnimalAttr.bornAfterSec = Time.timeSinceLevelLoad;
    }

    private float CalculateNewAvarage(float newValue, float exAvarage, int newArrayLength)
    {
        float exTotalValue = exAvarage * (newArrayLength - 1);
        return (exTotalValue + newValue) / (newArrayLength);
    }

    private void ResetAnimalAttributesInMinute()
    {
        animalAttributesInMinute.Add(avAnimalAttr);
        totalExistedAnimalsInMinute = 0;
        Debug.Log(avAnimalAttr.bornAfterSec);
        avAnimalAttr.resetVariables();
    }
}

