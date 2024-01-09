using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserShadow : TowerBase
{
    public string shadowOrbName = "Shadow orb name";
    public int shadowOrbAmount = 1;
    public float shadowOrbDamage = 10f;
    public float shadowOrbDurationTime = 5f;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.position);
            ProjectileShadow projectile = projectileGO.GetComponent<ProjectileShadow>();

            if (projectile != null)
            {
                projectile.target = enemyList[i].transform;
                projectile.damage = towerDamage;
            }
        }
    }

    public override void PerformAction()
    {
        for (int i = 0; i < shadowOrbAmount; i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(shadowOrbName, this.transform.position);
            ShadowOrb projectile = projectileGO.GetComponent<ShadowOrb>();

            projectile.shadowOrbDurationTime = shadowOrbDurationTime;
            projectile.shadowOrbDamage = shadowOrbDamage;
        }
    }
}