using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanicStorm : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private string triggerStunName;
    [SerializeField] private float abilityStunDuration;

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
        for (int i = 0; i < Mathf.Min(abilityProjectileAmount, enemyList.Count); i++)
        {
            int randomValue = (int)Random.Range(0f, enemyList.Count);

            triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
            StartCoroutine(TriggerProjectileCoroutine(triggerProjectileGO, enemyList[randomValue]));
        }
    }

    public override void AbilityFinishEffectAction(GameObject target)
    {
        // in future must be shock ability/visualisation like purify in wc3
        target.GetComponent<EnemyMove>().ApplyStun(triggerStunName, abilityStunDuration);
    }
}