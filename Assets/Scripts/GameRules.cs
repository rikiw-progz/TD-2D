using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameRules : MonoBehaviour
{
    public float experience = 0f;
    [SerializeField] private float experienceRequired = 10f;
    public int playerLevel = 0;

    [Header("Cards")]
    [SerializeField] private GameObject towerParent;
    [SerializeField] private GameObject[] cards;
    private readonly float cardPositionControl = 400f;

    [Header("Cards")]
    public int firstEssenceAmount = 0;
    [SerializeField] private TextMeshProUGUI firstEssenceAmountTxt;
    public int secondEssenceAmount = 0;
    [SerializeField] private TextMeshProUGUI secondEssenceAmountTxt;
    public int thirdEssenceAmount = 0;
    [SerializeField] private TextMeshProUGUI thirdEssenceAmountTxt;

    public TextMeshProUGUI timerText; // Assign a UI Text element in the Inspector
    private float elapsedTime = 0f;

    private void Start()
    {
        Application.targetFrameRate = 144;
        CardShow();

        GetFirstEssence(0);
        GetSecondEssence(0);
        GetThirdEssence(0);
    }

    private void Update()
    {
        TimeDisplay();
    }

    void TimeDisplay()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = minutes + ":" + seconds.ToString("00");
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
        experienceRequired += 10 + playerLevel*2;
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

    

    public void GetFirstEssence(int amount)
    {
        firstEssenceAmount += amount;
        firstEssenceAmountTxt.text = firstEssenceAmount.ToString();
    }

    public void GetSecondEssence(int amount)
    {
        secondEssenceAmount += amount;
        secondEssenceAmountTxt.text = secondEssenceAmount.ToString();
    }

    public void GetThirdEssence(int amount)
    {
        thirdEssenceAmount += amount;
        thirdEssenceAmountTxt.text = thirdEssenceAmount.ToString();
    }
}