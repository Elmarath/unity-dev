using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float x;
    public float y;
    public GameObject gameObject;
    // Update is called once per frame

    void Start() {
        Instantiate(gameObject, new Vector3(0, 0, 0), transform.rotation);
    }

    void Update()
    {
        x = Random.Range(0f, 10f);
        Debug.Log("X' in degeri: " + x);

    }
}
