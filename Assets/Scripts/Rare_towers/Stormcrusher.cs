using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stormcrusher : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private AbilityTriggerRange _myAbilityTrigger;
    [SerializeField] private float thunderPosY = 10f;
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
        // Thunder to random unit in ability trigger range
        int randomValue = (int)Random.Range(0f, _myAbilityTrigger.abilityTriggerEnemyList.Count);

        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, new Vector2(_myAbilityTrigger.abilityTriggerEnemyList[randomValue].transform.position.x,_myAbilityTrigger.abilityTriggerEnemyList[randomValue].transform.position.y + thunderPosY));
        
        StartCoroutine(TriggerProjectileCoroutine(triggerProjectileGO, _myAbilityTrigger.abilityTriggerEnemyList[randomValue]));
    }

    public override void AbilityProjectileFinishEffect(GameObject target)
    {
        target.GetComponent<EnemyMove>().ApplyStun(triggerStunName, abilityStunDuration);
    }
}