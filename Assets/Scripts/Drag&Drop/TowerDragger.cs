using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TowerDragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _canvas;
    private Vector2 _dragBeginPosition;
    public Transform _parent;

    private void Start()
    {
        _canvas = transform.parent.root.GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parent = this.transform.parent;
        _dragBeginPosition = this.transform.position; 
        transform.parent.GetComponent<DropHandler>().enabled = false;

        this.transform.SetParent(this.transform.parent.parent);                                         // To show above other sprites

        this.GetComponent<Image>().raycastTarget = false;
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
        this.transform.SetParent(_parent);
        this.transform.position = _dragBeginPosition;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.GetComponent<Image>().raycastTarget = true;
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<TowerBase>().enabled = true;
        _parent.GetComponent<DropHandler>().enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}