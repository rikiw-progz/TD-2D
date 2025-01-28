using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPathing : MonoBehaviour
{
    public int enemyWaveCount = 15;
    public float timeBetweenEnemyWaves = 5f;
    private int enemyWaveCounter = 1;
    public float enemyWaveAmount = 10f;
    [SerializeField] private Transform enemyStartPosition;
    public float enemySpeed = 5f;
    public float enemyStartHP = 50f;
    private float _enemyHP = 50f;
    [SerializeField] private int enemyArmor = 0;
    private int _enemyArmor = 0;
    public float enemyBetweenEnemyDelayTime = 2f;
    public bool waveIsOnProcess = false;

    [SerializeField] private Transform[] path;
    public Button nextWaveBtn;

    private IEnumerator Start()
    {
        _enemyHP = enemyStartHP;
        _enemyArmor = enemyArmor;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyWaveHandler());
        if (nextWaveBtn != null)
        {
            // Add a listener to the button's onClick event
            nextWaveBtn.onClick.AddListener(() => NextWave());
        }
    }

    IEnumerator EnemyWaveHandler()
    {
        if (enemyWaveCounter < enemyWaveCount)
        {
            for (int i = 0; i < enemyWaveAmount; i++)
            {
                GameObject enemy = EnemyPool.instance.GetEnemyObject("Enemy", enemyStartPosition.position);

                if (enemy != null)
                {
                    enemy.SetActive(true);
                    enemy.GetComponent<EnemyHealth>().enemyHP = _enemyHP;
                    enemy.GetComponent<EnemyHealth>().armor = _enemyArmor;
                    enemy.GetComponent<EnemyMove>().enabled = true;
                    enemy.GetComponent<EnemyMove>().speed = enemySpeed;       // is this must be in Update???
                    if (enemy.GetComponent<EnemyMove>().pathAdded == false)
                    {
                        for (int j = 0; j < path.Length; j++)
                            enemy.GetComponent<EnemyMove>().enemyPath.Add(path[j]);
                    }

                    enemy.GetComponent<EnemyMove>().pathAdded = true;
                    waveIsOnProcess = true;

                    yield return new WaitForSeconds(enemyBetweenEnemyDelayTime);
                }
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

    void NextWave()
    {
        if (waveIsOnProcess == false)
        {
            StartCoroutine(EnemyWaveHandler());
            enemyWaveCounter++;
        }
    }

    void NextWaveLevelUp()
    {
        _enemyHP *= 1.2f;

        if(enemyBetweenEnemyDelayTime > 0.5f)
            enemyBetweenEnemyDelayTime -= 0.05f;

        if(enemySpeed < 3f)
            enemySpeed += 0.1f;
    }
}