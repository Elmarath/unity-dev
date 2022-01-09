using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    public List<float> valueList;

    private void OnEnable()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        foreach (Transform child in graphContainer.transform)
        {
            if (child.name != "Background")
                GameObject.Destroy(child.gameObject);
        }
    }

    public void Init(List<float> list)
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        valueList = null;
        valueList = list;
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("Circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<float> valueList)
    {
        float yMinimum = 60f;
        float xSize = 50f;
        float graphHeight = graphContainer.sizeDelta.y - yMinimum;

        GameObject lastCircleGameObject = null;

        int start = valueList.Count - 20;
        if (start < 0)
            start = 0;
        int finish = valueList.Count;
        int cycleCount = finish - start;


        for (int i = 0; i < cycleCount; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i + start] / yMinimum) * graphHeight + yMinimum / 2;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = dotPositionB - dotPositionA;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir.normalized * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0f, 0f, AngleBetweenVector2(dotPositionA, dotPositionB));
    }

    private float AngleBetweenVector2(Vector2 a, Vector2 b)
    {
        float x = b.x - a.x;
        float y = b.y - a.y;
        return Mathf.Atan2(y, x) * (180 / Mathf.PI);
    }

}
