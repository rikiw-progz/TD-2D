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

    [Header("Chakrum Tower")]
    [SerializeField] private GameObject[] chakrumTowers;
    [SerializeField] private TextMeshProUGUI ChakrumTowerAmountTMP;
    public int chakrumTowerAmount;
    private int chakrumTowerLevel = 1;

    [Header("Splash Tower")]
    [SerializeField] private GameObject[] splashTowers;
    [SerializeField] private TextMeshProUGUI splashTowerAmountTMP;
    public int splashTowerAmount;
    //private int splashTowerLevel = 1;

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

            if(simpleTowerLevel % 4 == 0)
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

    public void ChakrumTower(int amount)
    {
        Time.timeScale = 1f;

        cardParent.SetActive(false);

        chakrumTowerAmount += amount;

        ChakrumTowerAmountTMP.text = chakrumTowerAmount.ToString();

        foreach (GameObject g in chakrumTowers)
        {
            g.GetComponent<ChakrumTower>().towerDamage += 10f;

            if (chakrumTowerLevel % 3 == 0)
            {
                g.GetComponent<ChakrumTower>().bounceAmount += 1;
            }
        }
    }

    public void SplashTower(int amount)
    {
        Time.timeScale = 1f;

        cardParent.SetActive(false);

        splashTowerAmount += amount;

        splashTowerAmountTMP.text = splashTowerAmount.ToString();

        foreach (GameObject g in splashTowers)
        {
            g.GetComponent<SplashTower>().towerDamage += 10f;
        }
    }
}