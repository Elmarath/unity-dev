using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float getEatenTime = 5;

    [SerializeField]
    private float reGrowDelay = 15;
    [SerializeField]
    private float eatenScaleDivider = 3;
    [SerializeField]
    private Color eatenColor;
    [SerializeField]
    private Color edibleColor;

    private Renderer _spriteRenderer;
    private Vector3 edibleScale;
    private Vector3 eatenScale;

    public bool isEatable = true;
    public bool isStartedToBeEaten = false;

    void Awake()
    {
        _spriteRenderer = this.transform.GetComponent<Renderer>();
        edibleScale = this.transform.localScale;
        eatenScale = edibleScale / eatenScaleDivider;
    }

    private void Start()
    {
        this.transform.localScale = edibleScale;
        this._spriteRenderer.material.color = edibleColor;
    }

    public bool GetEaten()
    {
        if (isEatable)
        {
            isStartedToBeEaten = true;
            StartCoroutine(GetEatenCoroutine());
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator GetEatenCoroutine()
    {
        yield return new WaitForSeconds(getEatenTime);
        isEatable = false;
        isStartedToBeEaten = false;
        this.transform.localScale = eatenScale;
        this._spriteRenderer.material.color = eatenColor;
        StartCoroutine("ReGrowCoroutine");
    }

    IEnumerator ReGrowCoroutine()
    {
        yield return new WaitForSeconds(reGrowDelay);
        isEatable = true;
        this.transform.localScale = edibleScale;
        this._spriteRenderer.material.color = edibleColor;
    }
}