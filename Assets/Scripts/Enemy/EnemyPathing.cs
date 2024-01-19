using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public float enemyWaveAmount = 200f;
    [SerializeField] private Transform enemyStartPosition;
    public float enemySpeed = 5f;
    public float _enemyStartHP = 5f;
    private float _enemyHP = 5f;
    public float enemyBetweenEnemyDelay = 0.5f;

    [SerializeField] private Transform[] path;

    private IEnumerator Start()
    {
        _enemyHP = _enemyStartHP;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyWaveHandler());
    }

    IEnumerator EnemyWaveHandler()
    {
        for(int i = 0; i < enemyWaveAmount; i++)
        {
            GameObject enemy = EnemyPool.instance.GetEnemyObject("Enemy", enemyStartPosition.position);

            if (enemy != null)
            {
                enemy.SetActive(true);
                enemy.GetComponent<EnemyHealth>().enemyHP = _enemyHP;
                enemy.GetComponent<EnemyMove>().enabled = true;
                enemy.GetComponent<EnemyMove>().speed = enemySpeed;       // is this must be in Update???
                if(enemy.GetComponent<EnemyMove>().pathAdded == false)
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

        StartCoroutine(EnemyWaveHandler());
    }

    void NextWaveLevelUp()
    {
        _enemyHP += 3f;
        enemyBetweenEnemyDelay -= 0.05f;
        enemySpeed += 0.05f;
    }
}