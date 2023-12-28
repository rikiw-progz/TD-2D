using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public float experience = 0f;
    [SerializeField] private float newCardLimit = 20f;

    [Header("Cards")]
    [SerializeField] private GameObject cardParent;

    [Header("Towers")]
    [SerializeField] private GameObject[] simpleTowers;
    [SerializeField] private GameObject[] piercingTowers;
    private int levelAmount = 0;

    private void Start()
    {
        CardChoose();
    }

    public void ExperienceGain(float exp)
    {
        experience += exp;

        if (experience >= newCardLimit)
        {
            newCardLimit += experience;
            levelAmount += 1;
            CardChoose();
        }
    }

    void CardChoose()
    {
        Time.timeScale = 0f;
        cardParent.SetActive(true);
    }
}