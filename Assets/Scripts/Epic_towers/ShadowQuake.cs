using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowQuake : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private float armorReductionAmount = 5f;
    [SerializeField] private float armorReductionDuration = 5f;
    [SerializeField] private float additionalArmorReductionAmount = 2f;
    [SerializeField] private float buffTime = 5f;
    private bool towerBuffed = false;

    public override void Shoot()
    {
        base.Shoot();                                                           // to make trigger

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileFinishTrigger(GameObject target)
    {
        if (target.activeInHierarchy)
        {
            target.GetComponent<EnemyHealth>().DebuffArmor(armorReductionAmount, armorReductionDuration, triggerProjectileName);
        }
    }

    public override void TowerKillTrigger(GameObject target)
    {
        if(!towerBuffed)
        {
            towerBuffed = true;
            StartCoroutine(BuffArmorReduction());
        }
    }

    IEnumerator BuffArmorReduction()
    {
        armorReductionAmount += additionalArmorReductionAmount;
        yield return new WaitForSeconds(buffTime);
        armorReductionAmount -= additionalArmorReductionAmount;

        towerBuffed = false;
    }
}