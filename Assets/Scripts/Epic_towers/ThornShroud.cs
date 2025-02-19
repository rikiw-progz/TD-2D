using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornShroud : TowerBase
{
    [SerializeField] private string triggerProjectileName;

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
        for (int i = 0; i < Mathf.Min(abilityProjectileAmount, enemyList.Count); i++)
        {
            int randomValue = (int)Random.Range(0f, enemyList.Count);

            triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
            StartCoroutine(TriggerProjectileCoroutine(triggerProjectileGO, enemyList[randomValue]));
        }
    }

    public override void TowerKillTrigger(GameObject target)
    {
        for (int i = 0; i < Mathf.Min(abilityProjectileAmount, enemyList.Count); i++)
        {
            int randomValue = (int)Random.Range(0f, enemyList.Count);

            triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
            StartCoroutine(TriggerProjectileCoroutine(triggerProjectileGO, enemyList[randomValue]));
        }
    }
}