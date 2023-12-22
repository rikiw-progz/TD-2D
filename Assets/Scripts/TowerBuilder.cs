using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] simpleTowers;

    public Vector2 towerPositionToBuild;
    [SerializeField] private ButtonHandler _buttonHandler;
    [SerializeField] private TextMeshProUGUI _simpleTowerAmount;
    private int towerQueue = 0;

    public void BuildTower()
    {
        if(_buttonHandler.simpleTowerAmount > 0)
        {
            _buttonHandler.simpleTowerAmount -= 1;
            _simpleTowerAmount.text = _buttonHandler.simpleTowerAmount.ToString();

            simpleTowers[0 + towerQueue].transform.position = towerPositionToBuild;
            simpleTowers[0 + towerQueue].SetActive(true);
            towerQueue += 1;
        }
        this.GetComponent<Button>().enabled = false;
    }
}