using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private float enemyWaveAmount = 200f;
    [SerializeField] private Transform enemyStartPosition;
    private float _enemyHP = 50f;

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
                yield return new WaitForSeconds(0.25f);
            }
        }

        yield return new WaitForSeconds(5f);
        _enemyHP += 50f;
        StartCoroutine(EnemyWaveHandler());
    }
}