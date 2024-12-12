using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TowerDragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _canvas;
    private Vector2 _dragBeginPosition;
    public bool dropRight = false;
    private Transform _parent;

    private void Start()
    {
        _canvas = transform.parent.root.gameObject.GetComponent<Canvas>();
        _parent = this.transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragBeginPosition = this.transform.position; 
        transform.parent.GetComponent<DropTowerHandler>().enabled = false;

        this.transform.SetParent(this.transform.parent.parent);


        this.GetComponent<Image>().raycastTarget = false;

        this.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
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
        if (dropRight)
        {
            this.gameObject.SetActive(false);
            _parent.GetComponent<DropHandler>().enabled = true;

        }
        this.transform.position = _dragBeginPosition;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.GetComponent<Image>().raycastTarget = true;
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<TowerBase>().enabled = true;
        this.transform.SetParent(_parent);
        transform.parent.GetComponent<DropTowerHandler>().enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}