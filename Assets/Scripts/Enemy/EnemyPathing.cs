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
    public float enemyBetweenEnemyDelay = 0.5f;

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
            nextWaveBtn.onClick.AddListener(() => StartCoroutine(EnemyWaveHandler()));
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

                    yield return new WaitForSeconds(enemyBetweenEnemyDelay);
                }
            }

            // Next wave
            NextWaveLevelUp();

            yield return new WaitForSeconds(5f);
            StartCoroutine(EnemyWaveHandler());
            enemyWaveCounter++;
        }
    }

    void NextWaveLevelUp()
    {
        _enemyHP *= 1.2f;

        //if(enemyBetweenEnemyDelay > 0.5f)
        //    enemyBetweenEnemyDelay -= 0.05f;

        if(enemySpeed < 3f)
            enemySpeed += 0.1f;
    }
}