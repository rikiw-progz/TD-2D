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
    private float bonkStartDamage;
    [SerializeField] private GameObject shadow;
    private int shadowLevel = 0;
    private float shadowStartDamage;
    [SerializeField] private GameObject zeus;
    private int zeusLevel = 0;
    private float zeusStartDamage;
    [SerializeField] private GameObject rider;
    private int riderLevel = 0;
    private float riderStartDamage;
    [SerializeField] private GameObject livingVolcano;
    private int livingVolcanoLevel = 0;
    private float livingVolcanoStartDamage;
    [SerializeField] private GameObject firelord;
    private int firelordLevel = 0;
    private float firelordStartDamage;

    private void Start()
    {
        bonkStartDamage = bonk.GetComponent<LesserBonk>().towerDamage;
        shadowStartDamage = shadow.GetComponent<LesserShadow>().towerDamage;
        zeusStartDamage = zeus.GetComponent<LesserZeus>().towerDamage;
        riderStartDamage = rider.GetComponent<LesserRider>().towerDamage;
        livingVolcanoStartDamage = livingVolcano.GetComponent<LesserLivingVolcano>().towerDamage;
        firelordStartDamage = firelord.GetComponent<LesserFireLord>().towerDamage;
    }

    public void BonkTowerPick()
    {
        TowerPick(bonk);

        bonkLevel += 1;
        bonk.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bonkLevel.ToString();

        bonk.GetComponent<LesserBonk>().towerDamage += bonkStartDamage / 2;
    }

    public void ShadowTowerPick()
    {
        TowerPick(shadow);

        shadowLevel += 1;
        shadow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shadowLevel.ToString();

        shadow.GetComponent<LesserShadow>().towerDamage += shadowStartDamage / 10;
    }

    public void ZeusTowerPick()
    {
        TowerPick(zeus);

        zeusLevel += 1;
        zeus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = zeusLevel.ToString();

        zeus.GetComponent<LesserZeus>().towerDamage += zeusStartDamage / 10;
        zeus.GetComponent<LesserZeus>().thunderAmount += 1;
    }

    public void RiderTowerPick()
    {
        TowerPick(rider);

        riderLevel += 1;
        rider.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = riderLevel.ToString();

        rider.GetComponent<LesserRider>().towerDamage += riderStartDamage / 5;
    }

    public void LivingVolcanoTowerPick()
    {
        TowerPick(livingVolcano);

        livingVolcanoLevel += 1;
        livingVolcano.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = livingVolcanoLevel.ToString();

        livingVolcano.GetComponent<LesserLivingVolcano>().towerDamage += livingVolcanoStartDamage;
    }

    public void FirelordTowerPick()
    {
        TowerPick(firelord);

        firelordLevel += 1;
        firelord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = firelordLevel.ToString();

        firelord.GetComponent<LesserFireLord>().towerDamage += firelordStartDamage / 5;
    }

    void TowerPick(GameObject tower)
    {
        if (!tower.activeInHierarchy)
            tower.SetActive(true);

        Time.timeScale = 1f;

        cardParent.SetActive(false);
    }
}