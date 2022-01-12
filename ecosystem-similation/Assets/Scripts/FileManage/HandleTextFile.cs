using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class HandleTextFile : MonoBehaviour
{
    [MenuItem("Tools/Write file")]
    static void WriteString()
    {
        string path = "Assets/Animals.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        Object asset = Resources.Load("Animals");
        Debug.Log("Printed to assests");
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/Animals.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public void PrintAnimals(List<Animal> animals)
    {
        FileUtil.DeleteFileOrDirectory("Assets/Animals.txt");
        string path = "Assets/Animals.txt";
        StreamWriter writer = new StreamWriter(path, true);
        for (int i = 0; i < animals.Count; i++)
        {
            string animalNo = "animal No:" + i.ToString();
            writer.WriteLine(animalNo);
            writer.WriteLine(animals[i].normalSpeed.ToString());
            writer.WriteLine(animals[i].waitTime.ToString());
            writer.WriteLine(animals[i].minSearchDistance.ToString());
            writer.WriteLine(animals[i].maxHunger.ToString());
            writer.WriteLine(animals[i].maxThirst.ToString());
            writer.WriteLine(animals[i].maxReproduceUrge.ToString());
            writer.WriteLine(animals[i].howManyChildren.ToString());
            writer.WriteLine(animals[i].gettingHungryRate.ToString());
            writer.WriteLine(animals[i].pregnantTimeRate.ToString());
            writer.WriteLine(animals[i].gettingFullMultiplier.ToString());
            writer.WriteLine(animals[i].bornAfterSec.ToString());
        }

        Debug.Log(animals[0].normalSpeed.ToString());
    }
}
