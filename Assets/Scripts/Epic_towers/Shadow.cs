using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    public float shadowOrbLifetime = 5f;
    private Vector2 moveDirection; // Stores the initial direction

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.position);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileFinishTrigger(GameObject target)
    {
        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);

        if (target != null)
        {
            // Calculate direction only once at the start
            moveDirection = (target.transform.position - transform.position).normalized;
        }

        StartCoroutine(MoveAndDestroy(triggerProjectileGO));
    }

    IEnumerator MoveAndDestroy(GameObject shadowOrb)
    {
        float elapsedTime = 0f;
        shadowOrb.GetComponent<ShadowOrb>().shadowOrbDamage = abilityDamage;

        while (elapsedTime < shadowOrbLifetime)
        {
            shadowOrb.transform.position += abilityProjectileSpeed * Time.deltaTime * (Vector3)moveDirection;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        shadowOrb.SetActive(false);
    }
}