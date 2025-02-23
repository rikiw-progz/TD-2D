using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image _image;
    public Color32 _startColor;
    public bool primaryPlacement = false;
    public bool placeIsBusy = false;

    public TowerMergeHandler towerMergeHandler;
    public bool readyToMerge = false;

    private TowerBase tower1;
    private TowerBase tower2;

    private GameRules _gameRules;

    void Start()
    {
        _image = GetComponent<Image>();
        _startColor = _image.color;
        _gameRules = GameObject.FindWithTag("Stage Manager").GetComponent<GameRules>();
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
            _image.color = Color.red;
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
        placeIsBusy = true;
        eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().primaryPlacement = false;
        eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().placeIsBusy = false;
        eventData.pointerDrag.GetComponent<TowerDragger>()._parent.GetComponent<DropHandler>().enabled = true;

        tower1 = this.transform.GetChild(0).GetComponent<TowerBase>();
        tower2 = eventData.pointerDrag.GetComponent<TowerBase>();

        this.GetComponent<Image>().color = _startColor;

        GameObject towerGO = towerMergeHandler.MergeTowers(tower1, tower2, this.transform.position);
        
        if (towerGO != null)
        {
            tower1.gameObject.SetActive(false);
            tower1.transform.SetParent(null);
            tower2.gameObject.SetActive(false);

            towerGO.transform.SetParent(this.transform);
            towerGO.GetComponent<TowerDragger>().enabled = true;
            Debug.Log(towerGO.name);
        }
    }

    private void MergeElementWithoutPlacing(PointerEventData eventData)
    {
        placeIsBusy = true;
        eventData.pointerDrag.GetComponent<DragHandler>().towerGO.SetActive(false);

        tower1 = this.transform.GetChild(0).GetComponent<TowerBase>();
        tower2 = eventData.pointerDrag.GetComponent<TowerBase>();

        this.GetComponent<Image>().color = _startColor;

        tower1.gameObject.SetActive(false);
        tower1.transform.SetParent(null);

        GameObject towerGO = towerMergeHandler.MergeTowers(tower1, tower2, this.transform.position);

        if (towerGO != null)
        {
            towerGO.transform.SetParent(this.transform);
            towerGO.GetComponent<TowerDragger>().enabled = true;
            Debug.Log(towerGO.name);
            //this.enabled = false; // temporary solution
        }

        eventData.pointerDrag.GetComponent<TowerHandler>().TowerAmount(-1);
        if (eventData.pointerDrag.GetComponent<TowerHandler>().towerAmount <= 0)
            eventData.pointerDrag.GetComponent<DragHandler>().enabled = false;
    }
}