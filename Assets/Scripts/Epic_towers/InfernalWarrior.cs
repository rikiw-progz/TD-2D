using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfernalWarrior : TowerBase
{
    [SerializeField] private string triggerProjectileName;
    [SerializeField] private string triggerProjectileEffectName;
    [SerializeField] private float slowDebuffAmountPercent = 20f;
    [SerializeField] private float slowDebuffDuration = 5f;
    private float fearAuraAreaScaleTime = 0.2f;

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
        StartCoroutine(FearAuraArea());

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].activeInHierarchy)
            {
                enemyList[i].GetComponent<EnemyMove>().ApplyMovementSlow(slowDebuffAmountPercent, slowDebuffDuration, triggerProjectileEffectName);
            }
        }
    }

    IEnumerator FearAuraArea()
    {
        triggerProjectileGO = PoolBase.instance.GetObject(triggerProjectileName, this.transform.position);
        triggerProjectileGO.transform.localScale = new Vector2(0f, 0f);
        triggerProjectileGO.transform.DOScale(new Vector2(1f, 1f), fearAuraAreaScaleTime);
        yield return new WaitForSeconds(fearAuraAreaScaleTime);
        triggerProjectileGO.SetActive(false);
    }
}