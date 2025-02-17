using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTreant : TowerBase
{
    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }
}