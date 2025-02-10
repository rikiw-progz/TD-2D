using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Enemy amount")]
    public int enemyAmount = 0;
    [SerializeField] private int enemyLimitAmount = 50;
    [SerializeField] private TextMeshProUGUI enemyAmountTxt;
    [SerializeField] private GameObject gameOverTxt;
    [SerializeField] private GameObject replay;

    private void Start()
    {
        Application.targetFrameRate = 144;
        CardShow();

        if (replay != null)
        {
            // Add a listener to the button's onClick event
            replay.GetComponent<Button>().onClick.AddListener(() => ReloadScene());
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Synergy");
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
        experienceRequired += 10;
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

    public void EnemyCount(int amount)
    {
        enemyAmount += amount;
        enemyAmountTxt.text = enemyAmount.ToString();
        if (enemyAmount > enemyLimitAmount)
        {
            gameOverTxt.SetActive(true);
            replay.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}