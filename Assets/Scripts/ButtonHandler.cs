using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    [Header("Simple Tower")]
    [SerializeField] private GameObject[] simpleTowers;
    [SerializeField] private TextMeshProUGUI simpleTowerAmountTMP;
    public int simpleTowerAmount;
    private int simpleTowerLevel = 1;

    [Header("Piercing Tower")]
    [SerializeField] private GameObject[] piercingTowers;
    [SerializeField] private TextMeshProUGUI piercingTowerAmountTMP;
    public int piercingTowerAmount;

    [Header("Long Range Tower")]
    [SerializeField] private GameObject[] longRangeTowers;
    [SerializeField] private TextMeshProUGUI longRangeTowerAmountTMP;
    public int longRangeTowerAmount;

    public void SimpleTower(int amount)
    {
        simpleTowerLevel += 1;

        Time.timeScale = 1f;

        cardParent.SetActive(false);

        simpleTowerAmount += amount;

        simpleTowerAmountTMP.text = simpleTowerAmount.ToString();

        foreach(GameObject g in simpleTowers)
        {
            g.GetComponent<TowerAttack>().towerDamage += 5f;

            if(simpleTowerLevel % 3 == 0)
            {
                g.GetComponent<TowerAttack>().bulletAmount += 1;
            }
        }
    }

    public void PiercingTower(int amount)
    {
        Time.timeScale = 1f;

        cardParent.SetActive(false);

        piercingTowerAmount += amount;

        piercingTowerAmountTMP.text = piercingTowerAmount.ToString();

        foreach (GameObject g in piercingTowers)
        {
            g.GetComponent<PiercingTower>().towerDamage += 50f;
        }
    }

    public void LongRangeTower(int amount)
    {
        Time.timeScale = 1f;

        cardParent.SetActive(false);

        longRangeTowerAmount += amount;

        longRangeTowerAmountTMP.text = longRangeTowerAmount.ToString();

        foreach (GameObject g in longRangeTowers)
        {
            //g.GetComponent<PiercingTower>().towerDamage += 50f;
        }
    }
}