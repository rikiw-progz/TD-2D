using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildstorm : TowerBase
{
    public override void Shoot()
    {
        int randomValue = (int)Random.Range(0f, enemyList.Count);

        projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
        StartCoroutine(LineRendererProjectileCoroutine(projectileGO, enemyList[randomValue]));
    }
}