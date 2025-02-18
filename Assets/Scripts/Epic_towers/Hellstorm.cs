using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellstorm : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private string fireBurnName;
    [SerializeField] private float fireBurnDamage;
    [SerializeField] private float abilityDebuffDuration;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(LineRendererProjectileCoroutine(projectileGO, enemyList[i], towerDamage));
        }
    }

    public override void TowerTrigger()
    {
        for (int i = 0; i < Mathf.Min(abilityProjectileAmount, enemyList.Count); i++)
        {
            int randomValue = (int)Random.Range(0f, enemyList.Count);

            triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
            StartCoroutine(LineRendererProjectileCoroutine(triggerProjectileGO, enemyList[randomValue], abilityDamage));

            abilityTriggerRandomValue = Random.Range(0f, 100f);

            if (abilityTriggerRandomValue < abilityFinishChancePercentage)
            {
                // Execute your action here
                enemyList[randomValue].GetComponent<EnemyHealth>().DebuffDamage(fireBurnDamage, abilityDebuffDuration, fireBurnName);
            }
        }
    }
}