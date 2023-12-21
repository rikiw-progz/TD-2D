using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float fullHP = 100f;
    private float enemyHP;

    private void OnEnable()
    {
        enemyHP = fullHP;
    }

    public void GetEnemyHP(float f)
    {
        enemyHP -= f;
        if (enemyHP <= 0)
            Death();
    }

    void Death()
    {
        this.gameObject.SetActive(false);
    }
}