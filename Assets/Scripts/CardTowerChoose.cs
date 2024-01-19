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

        bonk.GetComponent<LesserBonk>().towerDamage *= 1.5f;
        bonk.GetComponent<LesserBonk>().fireCooldown *= 0.9f;

        bonk.GetComponent<LesserBonk>().bonkStunDuration *= 1.05f;
    }

    public void ShadowTowerPick()
    {
        TowerPick(shadow);

        shadowLevel += 1;
        shadow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shadowLevel.ToString();

        shadow.GetComponent<LesserShadow>().towerDamage *= 1.1f;
        shadow.GetComponent<LesserShadow>().fireCooldown *= 0.95f;
    }

    public void ZeusTowerPick()
    {
        TowerPick(zeus);

        zeusLevel += 1;
        zeus.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = zeusLevel.ToString();

        zeus.GetComponent<LesserZeus>().towerDamage *= 1.1f;
        zeus.GetComponent<LesserZeus>().fireCooldown *= 0.95f;
        zeus.GetComponent<LesserZeus>().thunderAmount += 1;

        if(zeusLevel % 2 == 0)
        {
            zeus.GetComponent<LesserZeus>().chancePercentage += 5;
        }
    }

    public void RiderTowerPick()
    {
        TowerPick(rider);

        riderLevel += 1;
        rider.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = riderLevel.ToString();

        rider.GetComponent<LesserRider>().towerDamage *= 1.2f;
        rider.GetComponent<LesserRider>().fireCooldown *= 0.9f;

        if (riderLevel % 2 == 0)
        {
            rider.GetComponent<LesserRider>().chancePercentage += 5;
        }

        rider.GetComponent<LesserRider>().hammerSplashRadius *= 1.1f;
    }

    public void LivingVolcanoTowerPick()
    {
        TowerPick(livingVolcano);

        livingVolcanoLevel += 1;
        livingVolcano.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = livingVolcanoLevel.ToString();

        livingVolcano.GetComponent<LesserLivingVolcano>().towerDamage += livingVolcanoStartDamage;
        livingVolcano.GetComponent<LesserLivingVolcano>().fireCooldown *= 0.9f;

        livingVolcano.GetComponent<LesserLivingVolcano>().heatAuraDamage *= 1.1f;
    }

    public void FirelordTowerPick()
    {
        TowerPick(firelord);

        firelordLevel += 1;
        firelord.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = firelordLevel.ToString();

        firelord.GetComponent<LesserFireLord>().towerDamage *= 1.2f;
        firelord.GetComponent<LesserFireLord>().fireCooldown *= 0.9f;

        firelord.GetComponent<LesserFireLord>().fireLordLiquidFireDuration *= 1.2f;
        firelord.GetComponent<LesserFireLord>().fireLordLiquidFireDamage *= 1.05f;
    }

    void TowerPick(GameObject tower)
    {
        if (!tower.activeInHierarchy)
            tower.SetActive(true);

        Time.timeScale = 1f;

        cardParent.SetActive(false);
    }
}