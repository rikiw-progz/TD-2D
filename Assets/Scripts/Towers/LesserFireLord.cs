using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserFireLord : TowerBase
{
    public float fireLordLiquidFireDamage = 5f;
    public float fireLordLiquidFireDuration = 5f;
    public string fireLordDebuffName = "Firelord debuff name";

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.position);
            ProjectileFireLord projectile = projectileGO.GetComponent<ProjectileFireLord>();

            if (projectile != null)
            {
                projectile.target = enemyList[i].transform;
                projectile.damage = towerDamage;
                projectile.fireLordLiquidFireDamage = fireLordLiquidFireDamage;
                projectile.fireLordLiquidFireDuration = fireLordLiquidFireDuration;
                projectile.fireLordDebuffName = fireLordDebuffName;
            }
        }
    }
}