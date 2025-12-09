using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image _image;
    public Color32 _startColor;
    public bool primaryPlacement = false;
    public bool placeIsBusy = false;

    private TowerMergeHandler towerMergeHandler;
    public bool readyToMerge = false;

    private TowerBase tower1;
    private TowerBase tower2;

    void Awake()
    {
        towerMergeHandler = GameObject.FindWithTag("Stage Manager").GetComponent<TowerMergeHandler>();
    }

    void Start()
    {
        _image = GetComponent<Image>();
        _startColor = _image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && !primaryPlacement && eventData.pointerDrag.GetComponent<DragHandler>() && !placeIsBusy)
        {
            FirstPlacement(eventData);
        }
        else if (eventData.pointerDrag != null && primaryPlacement && eventData.pointerDrag.GetComponent<DragHandler>())
        {
            MergeElementWithoutPlacing(eventData);
        }
        else if(eventData.pointerDrag != null && primaryPlacement && !eventData.pointerDrag.GetComponent<DragHandler>())
        {
            MergePlacement(eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            _image.color = Color.grey;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _startColor;
    }

    private void FirstPlacement(PointerEventData eventData)
    {
        placeIsBusy = true;
        eventData.pointerDrag.GetComponent<DragHandler>().towerGO.SetActive(false);

        GameObject towerPrefab = PoolBase.instance.GetObject(eventData.pointerDrag.name + "Tower", this.transform.position);
        primaryPlacement = true;
        towerPrefab.transform.SetParent(this.transform);

        this.GetComponent<Image>().color = _startColor;

        eventData.pointerDrag.GetComponent<TowerHandler>().TowerAmount(-1);
        if (eventData.pointerDrag.GetComponent<TowerHandler>().towerAmount <= 0)
            eventData.pointerDrag.GetComponent<DragHandler>().enabled = false;
    }

    private void MergePlacement(PointerEventData eventData)
    {
        if (this.transform.childCount > 0)
        {
            tower1 = this.transform.GetChild(0).GetComponent<TowerBase>();
        }
        tower2 = eventData.pointerDrag.GetComponent<TowerBase>();

        GameObject towerGO = towerMergeHandler.MergeTowers(tower1, tower2, this.transform.position);
        
        if (towerGO != null)
        {
            eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().primaryPlacement = false;
            eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().placeIsBusy = false;
            eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().enabled = true;

            placeIsBusy = true;
            tower1.gameObject.SetActive(false);
            tower1.transform.SetParent(null);
            tower2.gameObject.SetActive(false);

            towerGO.transform.SetParent(this.transform);
            towerGO.GetComponent<TowerDragger>().enabled = true;
            Debug.Log(towerGO.name);
        }

        this.GetComponent<Image>().color = _startColor;
    }

    private void MergeElementWithoutPlacing(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragHandler>().towerGO.SetActive(false);

        tower1 = this.transform.GetChild(0).GetComponent<TowerBase>();
        tower2 = eventData.pointerDrag.GetComponent<TowerBase>();

        GameObject towerGO = towerMergeHandler.MergeTowers(tower1, tower2, this.transform.position);

        if (towerGO != null)
        {
            placeIsBusy = true;
            tower1.gameObject.SetActive(false);
            tower1.transform.SetParent(null);

            towerGO.transform.SetParent(this.transform);
            towerGO.GetComponent<TowerDragger>().enabled = true;

            eventData.pointerDrag.GetComponent<TowerHandler>().TowerAmount(-1);
            if (eventData.pointerDrag.GetComponent<TowerHandler>().towerAmount <= 0)
                eventData.pointerDrag.GetComponent<DragHandler>().enabled = false;

            Debug.Log(towerGO.name);
        }

        this.GetComponent<Image>().color = _startColor;
    }
}