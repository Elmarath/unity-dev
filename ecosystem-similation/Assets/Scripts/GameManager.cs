using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float timeScale;
    public List<Animal> livingAnimals = new List<Animal>();
    private float m_FixedDeltaTime;

    void Awake()
    {
        this.m_FixedDeltaTime = Time.fixedDeltaTime;
        this.timeScale = Time.timeScale;
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
    }

    public void addToLivingAnimals(Animal animal)
    {
        for (var i = livingAnimals.Count - 1; i > -1; i--)
        {
            if (livingAnimals[i] == null)
                livingAnimals.RemoveAt(i);
        }
        livingAnimals.Add(animal);
        livingAnimals = livingAnimals.Distinct().ToList();
    }
    public void removeFromLivingAnimals(Animal animal)
    {
        livingAnimals.Remove(animal);
        for (var i = livingAnimals.Count - 1; i > -1; i--)
        {
            if (livingAnimals[i] == null)
                livingAnimals.RemoveAt(i);
        }
        livingAnimals = livingAnimals.Distinct().ToList();
    }
}
