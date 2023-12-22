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
    [SerializeField] private GameObject[] towers;
    private int levelAmount = 0;

    public void ExperienceGain(float exp)
    {
        experience += exp;

        if (experience >= newCardLimit)
        {
            newCardLimit += experience + newCardLimit/10;
            levelAmount += 1;
            CardChoose();
        }
    }

    void CardChoose()
    {
        Time.timeScale = 0f;
        cardParent.SetActive(true);

        foreach(GameObject g in towers)
        {
            g.GetComponent<TowerAttack>().towerDamage += 5f;

            if (levelAmount % 3 == 0)
                g.GetComponent<TowerAttack>().fireCooldown -= 0.01f;
        }
    }
}