using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Color32 _startColor;

    void Start()
    {
        _startColor = this.GetComponent<Image>().color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.transform.SetParent(this.transform);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<CanvasGroup>().alpha = 1f;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.transform.SetParent(this.transform.parent);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().droppedRight = true;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<CanvasGroup>().interactable = true;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<Image>().raycastTarget = true;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<CircleCollider2D>().enabled = true;

            eventData.pointerDrag.GetComponent<DragAndDropHandler>().towerGO.GetComponent<TowerDragger>().enabled = true;

            this.GetComponent<Image>().color = _startColor;
            this.enabled = false;

            eventData.pointerDrag.GetComponent<TowerHandler>().TowerAmount(-1);
            if (eventData.pointerDrag.GetComponent<TowerHandler>().towerAmount <= 0)
                eventData.pointerDrag.GetComponent<DragAndDropHandler>().enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            this.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            this.GetComponent<Image>().color = _startColor;
        }
    }
}