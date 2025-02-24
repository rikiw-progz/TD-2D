using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stormcrusher : TowerBase
{
    [SerializeField] private string triggerProjectileName;
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
        int randomValue = (int)Random.Range(0f, enemyList.Count);

        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, new Vector2(enemyList[randomValue].transform.position.x, enemyList[randomValue].transform.position.y + thunderPosY));
        
        StartCoroutine(StormCrusherLineRendererProjectileCoroutine(triggerProjectileGO,
            new Vector2(enemyList[randomValue].transform.position.x, enemyList[randomValue].transform.position.y + thunderPosY), 
            enemyList[randomValue], 
            abilityDamage));
    }

    public IEnumerator StormCrusherLineRendererProjectileCoroutine(GameObject go, Vector2 startPos, GameObject target, float damage)
    {
        if (target != null && go.activeInHierarchy)
        {
            go.GetComponent<CustomLineRenderer>().CustomSetUpLine(startPos, target.transform.position);
            DoDamage(target, damage);

            abilityTriggerRandomValue = Random.Range(0f, 100f);

            if (abilityTriggerRandomValue < chancePercentage)
            {
                // Execute your action here
                target.GetComponent<EnemyMove>().ApplyStun(triggerStunName, abilityStunDuration);
            }

            yield return new WaitForSeconds(0.15f);
            go.SetActive(false);

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }
}