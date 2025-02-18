using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLineRenderer : MonoBehaviour
{
    private LineRenderer lr;
    //private Transform[] points;
    [SerializeField] private Texture[] electricTextures;
    private int textureAmount;
    [SerializeField] private float fps = 30f;
    private float fpsCounter;
    private Coroutine _coroutine;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        textureAmount = electricTextures.Length;
        fpsCounter = 1f / fps;
    }

    private void OnEnable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine); // Ensure the old one is stopped
        }
        _coroutine = StartCoroutine(ElectricLineRendererAnimation());
    }

    //public void SetUpLine(Transform[] points)
    //{
    //    lr.positionCount = points.Length;
    //    this.points = points;
    //}

    //private void SetPositions()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        lr.SetPosition(i, points[i].position);
    //    }
    //}

    public void CustomSetUpLine(Vector2 startPosition, Vector2 endPosition)
    {
        lr.positionCount = 2;
        SetPosition(startPosition, endPosition);
    }

    private void SetPosition(Vector2 startPosition, Vector2 endPosition)
    {
        lr.SetPosition(0, startPosition);
        lr.SetPosition(1, endPosition);
    }

    private IEnumerator ElectricLineRendererAnimation()
    {
        while (true) // Infinite loop inside the coroutine
        {
            for (int i = 0; i < textureAmount; i++)
            {
                lr.material.SetTexture("_MainTex", electricTextures[i]);
                yield return new WaitForSeconds(fpsCounter);
            }
        }
    }

    public void DisableCustomLineRenderer(float timeToDisable)
    {
        StartCoroutine(DisablingGO(timeToDisable));
    }

    private IEnumerator DisablingGO(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _coroutine = null;
    }
}