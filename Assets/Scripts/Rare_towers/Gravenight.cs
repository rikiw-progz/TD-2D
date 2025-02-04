using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravenight : TowerBase
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

    public override void ProjectileFinishTrigger(GameObject target)
    {
        // Need to add special effect for dying from this
        target.GetComponent<EnemyHealth>().Death();
    }
}