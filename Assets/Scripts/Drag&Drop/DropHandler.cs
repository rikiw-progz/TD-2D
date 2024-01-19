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
            eventData.pointerDrag.transform.SetParent(this.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            eventData.pointerDrag.transform.SetParent(this.transform.parent);
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().droppedRight = true;
            eventData.pointerDrag.GetComponent<CanvasGroup>().interactable = false;
            eventData.pointerDrag.GetComponent<Image>().raycastTarget = false;
            eventData.pointerDrag.GetComponent<DragAndDropHandler>().enabled = false;
            eventData.pointerDrag.GetComponent<CircleCollider2D>().enabled = true;
            this.GetComponent<Image>().color = _startColor;
            this.enabled = false;
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