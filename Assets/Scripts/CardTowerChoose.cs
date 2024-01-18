using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardTowerChoose : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    [Header("Towers")]
    [SerializeField] private GameObject bonk;
    private int bonkLevel = 0;
    [SerializeField] private GameObject shadow;
    private int shadowLevel = 0;
    [SerializeField] private GameObject zeus;
    private int zeusLevel = 0;
    [SerializeField] private GameObject rider;
    private int riderLevel = 0;
    [SerializeField] private GameObject livingVolcano;
    private int livingVolcanoLevel = 0;
    [SerializeField] private GameObject firelord;
    private int firelordLevel = 0;

    public void BonkTowerPick()
    {
        TowerPick(bonk, bonkLevel);
    }

    public void ShadowTowerPick()
    {
        TowerPick(shadow, shadowLevel);
    }

    public void ZeusTowerPick()
    {
        TowerPick(zeus, zeusLevel);

        zeus.GetComponent<LesserZeus>().thunderAmount += 1;
    }

    public void RiderTowerPick()
    {
        TowerPick(rider, riderLevel);
    }

    public void LivingVolcanoTowerPick()
    {
        TowerPick(livingVolcano, livingVolcanoLevel);
    }

    public void FirelordTowerPick()
    {
        TowerPick(firelord, firelordLevel);
    }

    void TowerPick(GameObject tower, int towerLevel)
    {
        if (!tower.activeInHierarchy)
            tower.SetActive(true);

        towerLevel += 1;
        tower.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = towerLevel.ToString();

        Time.timeScale = 1f;

        cardParent.SetActive(false);
    }
}