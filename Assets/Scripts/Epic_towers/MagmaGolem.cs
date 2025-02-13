using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaGolem : TowerBase
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

    public override void TowerKillTrigger(GameObject target)
    {
        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, target.transform.position);
        triggerProjectileGO.GetComponent<MagmaExplosion>().MagmaExplode(this.gameObject, target.transform.position, 50f);
    }
}