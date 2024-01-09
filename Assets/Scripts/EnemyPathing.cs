using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private float enemyWaveAmount = 200f;
    [SerializeField] private Transform enemyStartPosition;
    [SerializeField] private float enemySpeed = 5f;
    private float _enemyHP = 5000f;

    [SerializeField] private Transform[] path;

    private IEnumerator Start()
    {
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

                yield return new WaitForSeconds(0.25f);
            }
        }

        yield return new WaitForSeconds(5f);
        _enemyHP += 50f;
        StartCoroutine(EnemyWaveHandler());
    }
}