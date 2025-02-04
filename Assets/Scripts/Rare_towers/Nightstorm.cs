using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightstorm : TowerBase
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
        // Thunder to random unit in ability trigger range
        
    }

    public override void TriggerProjectileFinishEffect(GameObject target)
    {
        
    }
}