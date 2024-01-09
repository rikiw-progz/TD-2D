using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserBonk : TowerBase
{
    public float bonkStunDuration = 0.5f;

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.position);
            ProjectileBonk projectile = projectileGO.GetComponent<ProjectileBonk>();

            if (projectile != null)
            {
                projectile.target = enemyList[i].transform;
                projectile.damage = towerDamage;
                projectile.bonkStunDuration = bonkStunDuration;
            }
        }
    }
}