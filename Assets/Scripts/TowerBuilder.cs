using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    public Vector2 towerPositionToBuild;
    [SerializeField] private GameObject[] towers;
    [SerializeField] private ButtonHandler _buttonHandler;
    [SerializeField] private TextMeshProUGUI _towerAmount;
    private int towerQueue = 0;

    public void BuildSimpleTower()
    {
        if(_buttonHandler.simpleTowerAmount > 0)
        {
            _buttonHandler.simpleTowerAmount -= 1;
            _towerAmount.text = _buttonHandler.simpleTowerAmount.ToString();

            towers[0 + towerQueue].transform.position = towerPositionToBuild;
            towers[0 + towerQueue].SetActive(true);
            towerQueue += 1;
        }
        this.GetComponent<Button>().enabled = false;
    }

    public void BuildPiercingTower()
    {
        if (_buttonHandler.piercingTowerAmount > 0)
        {
            _buttonHandler.piercingTowerAmount -= 1;
            _towerAmount.text = _buttonHandler.piercingTowerAmount.ToString();

            towers[0 + towerQueue].transform.position = towerPositionToBuild;
            towers[0 + towerQueue].SetActive(true);
            towerQueue += 1;
        }
        this.GetComponent<Button>().enabled = false;
    }

    public void BuildChakrumTower()
    {
        if (_buttonHandler.chakrumTowerAmount > 0)
        {
            _buttonHandler.chakrumTowerAmount -= 1;
            _towerAmount.text = _buttonHandler.chakrumTowerAmount.ToString();

            towers[0 + towerQueue].transform.position = towerPositionToBuild;
            towers[0 + towerQueue].SetActive(true);
            towerQueue += 1;
        }
        this.GetComponent<Button>().enabled = false;
    }

    public void BuildSplashTower()
    {
        if (_buttonHandler.splashTowerAmount > 0)
        {
            _buttonHandler.splashTowerAmount -= 1;
            _towerAmount.text = _buttonHandler.splashTowerAmount.ToString();

            towers[0 + towerQueue].transform.position = towerPositionToBuild;
            towers[0 + towerQueue].SetActive(true);
            towerQueue += 1;
        }
        this.GetComponent<Button>().enabled = false;
    }
}