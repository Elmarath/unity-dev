using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Renderer _spriteRenderer;

    [SerializeField]
    private float fadeInTime;
    [SerializeField]
    private float fadeOutTime;
    [SerializeField]
    private float fadeDelay;
    [SerializeField]
    private Vector3 eatenScale;
    [SerializeField]
    private Color eatenColor;
    [SerializeField]
    private Color edibleColor;

    private Vector3 edibleScale;
    private Vector3 eatenTempScale; // garbage
    // Start is called before the first frame update

    public bool isEatable = true;

    void Awake()
    {
        _spriteRenderer = this.transform.GetComponent<Renderer>();
        edibleScale = this.transform.localScale;
    }

    private void Start()
    {
        if (isEatable)
        {
            GetEaten();
        }
    }

    public void GetEaten()
    {
        StartCoroutine(GetEatenCoroutine());
    }

    IEnumerator GetEatenCoroutine()
    {
        isEatable = false;
        for (float t = 0.01f; t < fadeInTime; t += 0.1f)
        {
            this.transform.localScale = Vector3.Lerp(edibleScale, eatenScale, t / fadeOutTime);
            _spriteRenderer.material.color = Color.Lerp(edibleColor, eatenColor, t / fadeOutTime);
            yield return null;
        }
        eatenTempScale = this.transform.localScale;
        _spriteRenderer.material.color = eatenColor;
        yield return new WaitForSeconds(fadeDelay);

        StartCoroutine(RegrowCoroutine());
    }

    IEnumerator RegrowCoroutine()
    {
        for (float t = 0.01f; t < fadeOutTime; t += 0.1f)
        {
            this.transform.localScale = Vector3.Lerp(eatenTempScale, edibleScale, t / fadeInTime);
            _spriteRenderer.material.color = Color.Lerp(eatenColor, edibleColor, t / fadeInTime);
            yield return null;
        }
        _spriteRenderer.material.color = edibleColor;
        this.transform.localScale = edibleScale;
        isEatable = true;
    }
}