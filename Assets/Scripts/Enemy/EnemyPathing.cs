using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyPathing : MonoBehaviour
{
    public int enemyWaveMaxAmount = 15;
    public float timeBetweenEnemyWaves = 5f;
    private int enemyWaveCounter = 1;
    [SerializeField] private TextMeshProUGUI enemyWaveCount;
    public float enemyAmountPerWave = 10f;
    [SerializeField] private BoxCollider2D spawnArea;

    public float enemySpeed = 5f;
    public float enemyStartHP = 50f;
    private float _enemyHP = 50f;
    [SerializeField] private int enemyArmor = 0;
    private int _enemyArmor = 0;
    public float enemyBetweenEnemyDelayTime = 2f;
    public bool waveIsOnProcess = false;
    public Button nextWaveBtn;

    [Header("Enemy amount")]
    public int enemyAmount = 0;
    //[SerializeField] private int enemyLimitAmount = 50;
    //[SerializeField] private TextMeshProUGUI enemyAmountTxt;
    [SerializeField] private GameObject gameOverTxt;
    [SerializeField] private GameObject replay;

    private IEnumerator Start()
    {
        _enemyHP = enemyStartHP;
        _enemyArmor = enemyArmor;

        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyWaveHandler());
        if (nextWaveBtn != null)
        {
            // Add a listener to the button's onClick event
            //nextWaveBtn.onClick.AddListener(() => NextWave());
        }

        if (replay != null)
        {
            // Add a listener to the button's onClick event
            replay.GetComponent<Button>().onClick.AddListener(() => ReloadScene());
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Synergy");
    }

    IEnumerator EnemyWaveHandler()
    {
        if (enemyWaveCounter < enemyWaveMaxAmount)
        {
            enemyWaveCount.text = enemyWaveCounter.ToString();
            for (int i = 0; i < enemyAmountPerWave; i++)
            {
                Vector2 spawnPos = GetRandomPointInCollider(spawnArea);

                GameObject enemy = EnemyPool.instance.GetEnemyObject("Enemy", spawnPos);

                if (enemy != null)
                {
                    enemy.SetActive(true);
                    enemy.GetComponent<EnemyHealth>().maxHealth = _enemyHP;
                    enemy.GetComponent<EnemyHealth>().enemyHP = _enemyHP;
                    enemy.GetComponent<EnemyHealth>().SetHealth(_enemyHP, _enemyHP);
                    enemy.GetComponent<EnemyHealth>().armor = _enemyArmor;
                    enemy.GetComponent<EnemyMove>().enabled = true;
                    enemy.GetComponent<EnemyMove>().speed = enemySpeed;

                    enemy.GetComponent<EnemyMove>().pathAdded = true;
                    waveIsOnProcess = true;

                    yield return new WaitForSeconds(enemyBetweenEnemyDelayTime);
                }
            }

            if (enemyWaveCounter >= 0 && enemyWaveCounter % 2 == 0) // Boss-1 (Every 2 waves, starting wave 6)
            {
                SpawnBoss("Boss_1", _enemyHP * 5, _enemyArmor * 2, enemySpeed * 0.8f);
                //EnemyCount(1); // for now
            }
            if (enemyWaveCounter >= 5 && enemyWaveCounter % 4 == 0) // Boss-2 (Every 4 waves, starting wave 10)
            {
                SpawnBoss("Boss_2", _enemyHP * 10, _enemyArmor * 4, enemySpeed * 0.7f);
                //EnemyCount(1); // for now
            }
            if (enemyWaveCounter >= 10 && enemyWaveCounter % 7 == 0) // Boss-3 (Every 7 waves, starting wave 15)
            {
                SpawnBoss("Boss_3", _enemyHP * 20, _enemyArmor * 6, enemySpeed * 0.6f);
                //EnemyCount(1); // for now
            }

            waveIsOnProcess = false;
            // Next wave upgrade
            NextWaveLevelUp();

            yield return new WaitForSeconds(timeBetweenEnemyWaves);
            if(waveIsOnProcess == false)
            {
                StartCoroutine(EnemyWaveHandler());
                enemyWaveCounter++;
            }
        }
    }

    public static Vector2 GetRandomPointInCollider(BoxCollider2D box)
    {
        Bounds b = box.bounds;

        float left = b.min.x;
        float right = b.max.x;
        float bottom = b.min.y;
        float top = b.max.y;

        float r = box.edgeRadius;

        left += r;
        right -= r;
        bottom += r;
        top -= r;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: return new Vector2(Random.Range(left, right), bottom); // bottom
            case 1: return new Vector2(Random.Range(left, right), top);    // top
            case 2: return new Vector2(left, Random.Range(bottom, top));   // left
            case 3: return new Vector2(right, Random.Range(bottom, top));  // right
        }

        return Vector2.zero;
    }

    void SpawnBoss(string bossType, float bossHP, float bossArmor, float bossSpeed)
    {
        Vector2 spawnPos = GetRandomPointInCollider(spawnArea);

        GameObject boss = EnemyPool.instance.GetEnemyObject(bossType, spawnPos);

        if (boss != null)
        {
            boss.SetActive(true);
            boss.GetComponent<EnemyHealth>().maxHealth = bossHP;
            boss.GetComponent<EnemyHealth>().enemyHP = bossHP;
            boss.GetComponent<EnemyHealth>().SetHealth(bossHP, bossHP);
            boss.GetComponent<EnemyHealth>().armor = bossArmor;
            boss.GetComponent<EnemyMove>().enabled = true;
            boss.GetComponent<EnemyMove>().speed = bossSpeed;
            boss.GetComponent<EnemyMove>().pathAdded = true;
        }
    }

    //void NextWave()
    //{
    //    if (waveIsOnProcess == false)
    //    {
    //        StartCoroutine(EnemyWaveHandler());
    //        enemyWaveCounter++;
    //    }
    //}

    void NextWaveLevelUp()
    {
        _enemyHP *= 1.15f;

        if(enemyBetweenEnemyDelayTime > 0.1f)
            enemyBetweenEnemyDelayTime -= 0.02f;

        if(enemySpeed < 3f)
            enemySpeed += 0.02f;

        if (enemyAmountPerWave < 2000)
            enemyAmountPerWave++;
    }

    //public void EnemyCount(int amount)
    //{
    //    enemyAmount += amount;
    //    enemyAmountTxt.text = enemyAmount.ToString();
    //    if (enemyAmount > enemyLimitAmount)
    //    {
    //        gameOverTxt.SetActive(true);
    //        replay.SetActive(true);
    //        Time.timeScale = 0f;
    //    }
    //}
}