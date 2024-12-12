using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    private Transform startParent;
    private Vector2 _dragBeginPosition;
    public bool droppedRight = false;
    public GameObject towerGO;

    private void Start()
    {
        this.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.GetComponent<TowerHandler>().towerAmount <= 0)
            return;

        _dragBeginPosition = this.transform.position;
        this.GetComponent<Image>().raycastTarget = false;

        towerGO = PoolBase.instance.GetObject(this.gameObject.name, this.transform.localPosition);
        startParent = towerGO.transform.parent;
        towerGO.transform.SetParent(this.transform);
        towerGO.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        towerGO.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        towerGO.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        towerGO.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
        towerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        towerGO.GetComponent<CanvasGroup>().alpha = 0.5f;
        towerGO.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        towerGO.GetComponent<RectTransform>().anchoredPosition += eventData.delta/_canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedRight)
        {
            towerGO.transform.position = _dragBeginPosition;
            towerGO.SetActive(false);
            towerGO.transform.SetParent(startParent);
        }

        droppedRight = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}