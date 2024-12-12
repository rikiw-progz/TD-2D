using UnityEngine;

public class WaterTower : TowerBase
{
    private GameObject projectileGO;

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    private void OnDisable()
    {
        if(projectileGO != null)
            projectileGO.SetActive(false);
    }
}