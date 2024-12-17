using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropTowerHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TowerMergeHandler towerMergeHandler;
    public bool readyToMergeToCommon = false;

    private TowerBase tower1;
    private TowerBase tower2;

    private Color32 _startColor;

    void Start()
    {
        _startColor = this.GetComponent<Image>().color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<TowerDragger>() && readyToMergeToCommon)
        {
            tower1 = this.transform.GetChild(0).GetComponent<TowerBase>();
            tower2 = eventData.pointerDrag.GetComponent<TowerBase>();

            this.GetComponent<Image>().color = _startColor;
            //this.enabled = false;
            tower1.gameObject.SetActive(false);
            GameObject towerGO = towerMergeHandler.MergeTowers(tower1, tower2, this.transform.position);
            if(towerGO != null)
            {
                towerGO.transform.SetParent(this.transform);
                this.enabled = false;
                Debug.Log(towerGO.name);
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<TowerDragger>() && readyToMergeToCommon)
        {
            this.GetComponent<Image>().color = Color.green;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<TowerDragger>() && readyToMergeToCommon)
        {
            this.GetComponent<Image>().color = _startColor;
        }
    }
}