using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerChooseHandler : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    [Header("Towers")]
    [SerializeField] private GameObject natureTower;
    [SerializeField] private GameObject darknessTower;
    [SerializeField] private GameObject thunderTower;
    [SerializeField] private GameObject earthTower;
    [SerializeField] private GameObject fireTower;

    public void NatureTowerPick()
    {
        TowerPick(natureTower);
    }

    public void DarknessTowerPick()
    {
        TowerPick(darknessTower);
    }

    public void ThunderTowerPick()
    {
        TowerPick(thunderTower);
    }

    public void EarthTowerPick()
    {
        TowerPick(earthTower);
    }

    public void FireTowerPick()
    {
        TowerPick(fireTower);
    }

    void TowerPick(GameObject tower)
    {
        tower.GetComponent<TowerHandler>().TowerAmount(1);

        if (tower.GetComponent<TowerHandler>().towerAmount > 0)
            tower.GetComponent<DragHandler>().enabled = true;

        Time.timeScale = 1f;

        cardParent.SetActive(false);
    }
}