using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dreadstone : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private float slowDebuffAmountPercent = 20f;
    [SerializeField] private float slowDebuffDuration = 5f;
    private GameObject target;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
            target = enemyList[i];
        }
    }

    public override void ProjectileFinishTrigger()
    {
        base.ProjectileFinishTrigger();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            if (target.activeInHierarchy)
            {
                target.GetComponent<EnemyMove>().ApplyMovementSlow(slowDebuffAmountPercent, slowDebuffDuration, triggerProjectileName);
            }
        }
    }
}