using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserRider : TowerBase
{
    public string riderHammerProjectileName = "Rider hammer projectile";
    public float riderHammerDamage = 10f;
    public float riderHammerStunDuration = 0.25f;
    public string riderHammerSplashName = "Rider hammer splash";
    public float hammerSplashRadius = 1f;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));

            float randomValue = Random.Range(0f, 100f);

            if (randomValue < chancePercentage)
            {
                // Execute your action here
                HammerTrigger(enemyList[i]);
            }
            else
            {
                // Action did not occur
            }
        }
    }

    private void HammerTrigger(GameObject target)
    {
        GameObject projectileGO = PoolBase.instance.GetEnemyObject(riderHammerProjectileName, this.transform.localPosition);
        StartCoroutine(ProjectileCoroutine(projectileGO, target));
        projectileFinishEffect = true;
    }

    public override void ProjectileFinish(GameObject target)
    {
        projectileFinishEffect = false;
        GameObject hammerGO = PoolBase.instance.GetEnemyObject(riderHammerSplashName, target.transform.localPosition);
        SplashAttack(hammerGO);
    }

    private void SplashAttack(GameObject target)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(target.transform.position, hammerSplashRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().GetEnemyHP(riderHammerDamage);
                collider.GetComponent<EnemyMove>().Stun(riderHammerStunDuration);
            }
        }
        StartCoroutine(SplashEffectDisable(target));
    }

    IEnumerator SplashEffectDisable(GameObject hammer)
    {
        yield return new WaitForSeconds(0.25f);
        hammer.SetActive(false);
    }
}