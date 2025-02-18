using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volter : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private int bounceAmount = 4;

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
        int randomValue = (int)Random.Range(0f, enemyList.Count);

        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);

        triggerProjectileGO.GetComponent<ElectricFireball>().PerformElectricFireball(enemyList[randomValue], abilityDamage, bounceAmount);
    }


}