using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightstorm : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private int maxBounceCount;

    public override void Shoot()
    {
        base.Shoot();
        
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(LineRendererProjectileCoroutine(projectileGO, enemyList[i], towerDamage));
        }
    }

    public override void ProjectileFinishTrigger(GameObject target)
    {
        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, target.transform.position);
        if (target != null && projectileGO.activeInHierarchy)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            projectileGO.GetComponent<CustomLineRenderer>().CustomSetUpLine(this.transform.position, target.transform.position);
        }
        triggerProjectileGO.GetComponent<ChainLightning>().PerformChainLightning(target, projectileGO, abilityDamage, maxBounceCount);
    }
}