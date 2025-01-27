using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stormblazer : TowerBase
{
    public int burstAttackAmount = 3;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void TowerTrigger()
    {
        burstAttackAmount = 3;
        StartCoroutine(BurstAttack());
    }

    IEnumerator BurstAttack()
    {
        int randomValue = (int)Random.Range(0f, enemyList.Count);

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[randomValue]));
        }

        burstAttackAmount--;
        yield return new WaitForSeconds(0.1f);

        if(burstAttackAmount > 0)
        {
            StartCoroutine(BurstAttack());
        }
    }
}