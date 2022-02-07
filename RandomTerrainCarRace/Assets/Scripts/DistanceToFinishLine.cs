using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToFinishLine : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    public GameObject finishLine;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToFinishLine();
    }

    void CalculateDistanceToFinishLine()
    {
        float furtherCarPositionZ;

        if (car1.transform.position.z > car2.transform.position.z)
        {
            furtherCarPositionZ = car1.transform.position.z;
        }
        else
        {
            furtherCarPositionZ = car2.transform.position.z;
        }

        float distanceToFinishLine = finishLine.transform.position.z - furtherCarPositionZ;

        float xSize = 20f - (distanceToFinishLine * 20f / 470f);
        if(xSize < 0f)
        {
            xSize = 0f;
        }
        float ySize = rt.localScale.y;
        float zSize = rt.localScale.z;
        rt.localScale = new Vector3(xSize, ySize, zSize);
    }
}
