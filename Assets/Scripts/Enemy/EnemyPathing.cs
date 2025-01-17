using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPathing : MonoBehaviour
{
    public int enemyWaveCount = 15;
    private int enemyWaveCounter = 1;
    public float enemyWaveAmount = 10f;
    [SerializeField] private Transform enemyStartPosition;
    public float enemySpeed = 5f;
    public float _enemyStartHP = 5f;
    private float _enemyHP = 5f;
    public float enemyBetweenEnemyDelayTime = 2f;
    public bool waveIsOnProcess = false;

    [SerializeField] private Transform[] path;
    public Button nextWaveBtn;

    private IEnumerator Start()
    {
        _enemyHP = _enemyStartHP;
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

            yield return new WaitForSeconds(5f);
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