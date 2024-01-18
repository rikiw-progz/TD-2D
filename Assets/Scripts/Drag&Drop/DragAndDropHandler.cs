using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private Vector2 _dragBeginPosition;
    public bool droppedRight = false;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = this.transform.parent;
        _dragBeginPosition = this.transform.position;
        transform.SetParent(transform.root);

        _canvasGroup.alpha = 0.5f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedRight)
        {
            this.transform.position = _dragBeginPosition;
            this.transform.SetParent(_originalParent);
        }
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        droppedRight = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}