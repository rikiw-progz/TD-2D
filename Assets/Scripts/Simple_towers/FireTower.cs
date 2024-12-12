using UnityEngine;

public class FireTower : TowerBase
{
    private GameObject projectileGO;

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    private void OnDisable()
    {
        if (projectileGO != null)
            projectileGO.SetActive(false);
    }
}