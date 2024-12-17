using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    public GameObject towerGO;

    private void Start()
    {
        this.enabled = false;
        towerGO = this.transform.GetChild(0).gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.GetComponent<TowerHandler>().towerAmount <= 0)
            return;

        this.GetComponent<Image>().raycastTarget = false;

        towerGO.SetActive(true);

        // Set towerGO position to match where the drag started
        RectTransform thisRectTransform = this.GetComponent<RectTransform>();
        RectTransform towerRectTransform = towerGO.GetComponent<RectTransform>();
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(thisRectTransform, eventData.position, _canvas.worldCamera, out localMousePosition);
        towerRectTransform.anchoredPosition = localMousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        towerGO.GetComponent<RectTransform>().anchoredPosition += eventData.delta/_canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        towerGO.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}