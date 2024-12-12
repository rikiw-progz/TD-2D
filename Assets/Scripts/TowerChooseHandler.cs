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
    [SerializeField] private GameObject waterTower;
    [SerializeField] private GameObject earthTower;
    [SerializeField] private GameObject fireTower;

    private void Start()
    {
        //bonkStartDamage = natureTower.GetComponent<LesserBonk>().towerDamage;
        //shadowStartDamage = darknessTower.GetComponent<LesserShadow>().towerDamage;
        //zeusStartDamage = thunderTower.GetComponent<LesserZeus>().towerDamage;
        //riderStartDamage = waterTower.GetComponent<LesserRider>().towerDamage;
        //livingVolcanoStartDamage = earthTower.GetComponent<LesserLivingVolcano>().towerDamage;
        //firelordStartDamage = fireTower.GetComponent<LesserFireLord>().towerDamage;
    }

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

    public void WaterTowerPick()
    {
        TowerPick(waterTower);
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
            tower.GetComponent<DragAndDropHandler>().enabled = true;

        Time.timeScale = 1f;

        cardParent.SetActive(false);
    }
}