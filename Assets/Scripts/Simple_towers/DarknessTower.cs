using UnityEngine;

public class DarknessTower : TowerBase
{
    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }
}