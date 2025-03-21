using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightflame : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private float armorReductionAmount = 3f;
    [SerializeField] private float armorReductionDuration = 5f;

    public override void Shoot()
    {
        base.Shoot();                                                           // to make trigger

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileFinishTrigger(GameObject target)
    {
        if (target.activeInHierarchy)
        {
            target.GetComponent<EnemyHealth>().DebuffArmor(armorReductionAmount, armorReductionDuration, triggerProjectileName);
        }
    }
}