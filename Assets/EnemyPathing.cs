using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private Transform enemyStartPosition;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyWaveHandler());
    }

    IEnumerator EnemyWaveHandler()
    {
        for(int i = 0; i < 200; i++)
        {
            GameObject enemy = EnemyPool.instance.GetEnemyObject("Enemy", enemyStartPosition.position);

            if (enemy != null)
            {
                enemy.SetActive(true);
                enemy.GetComponent<EnemyMove>().enabled = true;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(EnemyWaveHandler());
        }
    }
}