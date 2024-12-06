using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public float experience = 0f;
    [SerializeField] private float experienceRequired = 10f;
    public int playerLevel = 0;

    [Header("Cards")]
    [SerializeField] private GameObject towerParent;
    [SerializeField] private GameObject[] cards;
    private readonly float cardPositionControl = 400f;

    private void Start()
    {
        Application.targetFrameRate = 144;
        CardShow();
    }

    public void ExperienceGain(float exp)
    {
        experience += exp;

        if (experience >= experienceRequired)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        playerLevel += 1;
        experienceRequired += playerLevel * 10 + 10;
        CardShow();
    }

    void CardShow()
    {
        ShowRandomCards();

        towerParent.SetActive(true);
        Time.timeScale = 0f;
    }

    void ShowRandomCards()
    {
        ShuffleArray(cards);

        for (int i = 0; i < cards.Length; i++)
        {
            if (i < 3)
            {
                cards[i].SetActive(true);

                cards[i].transform.localPosition = new Vector2(-400f + i * cardPositionControl, 0f);
            }
            else
            {
                cards[i].SetActive(false);
            }
        }
    }

    void ShuffleArray(GameObject[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}