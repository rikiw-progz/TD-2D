using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject cardParent;
    [SerializeField] private TextMeshProUGUI simpleTowerAmountTMP;
    public int simpleTowerAmount;

    public void SimpleTower(int amount)
    {
        Time.timeScale = 1f;

        cardParent.SetActive(false);

        simpleTowerAmount += amount;

        simpleTowerAmountTMP.text = simpleTowerAmount.ToString();
    }
}