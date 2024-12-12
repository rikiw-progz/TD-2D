using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TowerDragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _canvas;
    private Vector2 _dragBeginPosition;
    public bool dropRight = false;

    private void Start()
    {
        _canvas = transform.parent.root.gameObject.GetComponent<Canvas>();
        Debug.Log(_canvas.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragBeginPosition = this.transform.position;

        this.GetComponent<Image>().raycastTarget = false;

        //this.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        //this.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        this.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        this.GetComponent<CanvasGroup>().alpha = 0.5f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;

        this.GetComponent<TowerBase>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dropRight)
        {
            this.transform.position = _dragBeginPosition;
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.GetComponent<Image>().raycastTarget = true;
            this.GetComponent<CanvasGroup>().alpha = 1f;
            this.GetComponent<TowerBase>().enabled = true;
        }

        dropRight = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}