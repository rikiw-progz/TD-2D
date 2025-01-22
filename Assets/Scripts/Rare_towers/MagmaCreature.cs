using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaCreature : TowerBase
{
    private GameObject projectileGO;
    private GameObject triggerProjectileGO;
    [SerializeField] private string triggerProjectileName;

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
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
            StartCoroutine(TriggerProjectileCoroutine(triggerProjectileGO, enemyList[i]));
        }
    }

    private void OnDisable()
    {
        if (projectileGO != null)
            projectileGO.SetActive(false);

        if (triggerProjectileGO != null)
            triggerProjectileGO.SetActive(false);
    }
}