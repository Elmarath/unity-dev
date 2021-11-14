using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float getEatenTime = 5;

    [SerializeField]
    private float reGrowDelay = 15;
    [SerializeField]
    private Vector3 eatenScale;
    [SerializeField]
    private Color eatenColor;
    [SerializeField]
    private Color edibleColor;

    private Renderer _spriteRenderer;
    private Vector3 edibleScale;

    public bool isEatable = true;

    void Awake()
    {
        _spriteRenderer = this.transform.GetComponent<Renderer>();
        edibleScale = this.transform.localScale;
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