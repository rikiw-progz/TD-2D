using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hellhound : TowerBase
{
    private int randomTowerIndex;
    private List<Collider2D> towersInRange = new();
    [SerializeField] private string abilityAttackDamageName;
    [SerializeField] private float attackDamagePercent = 20f;
    [SerializeField] private float attackDamageDuration = 5f;
    [SerializeField] private string abilityAttackDamageAreaName;
    [SerializeField] private float abilityAttackDamageAreaScaleTime = 0.3f;
    private float randomNumber;

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
        DetectTowers();
    }

    private void DetectTowers()
    {
        Vector2 myPosition = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(myPosition, towerRadius);

        foreach (Collider2D hit in hits)
        {
            // ignore enemies
            if (hit.CompareTag("Enemy"))
                continue;

            Vector2 towerPosition = hit.transform.position;

            // Check if the actual distance is within range
            if (Vector2.Distance(myPosition, towerPosition) <= towerRadius)
            {
                towersInRange.Add(hit);
            }
        }

        ChooseTower();
    }

    private void ChooseTower()
    {
        randomTowerIndex = Random.Range(0, towersInRange.Count);

        ApplyDamageBuff(towersInRange[randomTowerIndex].gameObject);
    }

    private void ApplyDamageBuff(GameObject tower)
    {
        if(tower.GetComponent<TowerBase>())
        {
            tower.GetComponent<TowerBase>().ApplyAttackDamageBuff(attackDamagePercent, attackDamageDuration, abilityAttackDamageName);
            ApplyAreaDamageBuff();
        }
        else
        {
            Debug.Log("This is not tower!");
        }
    }

    private void ApplyAreaDamageBuff()
    {
        randomNumber = Random.Range(0f, 100f);

        if (randomNumber < chancePercentage)
        {
            StartCoroutine(AttackDamageBuffArea());

            foreach(Collider2D towerInRange in towersInRange)
                towerInRange.GetComponent<TowerBase>().ApplyAttackDamageBuff(attackDamagePercent, attackDamageDuration, abilityAttackDamageName);
        }

        towersInRange.Clear();
    }

    IEnumerator AttackDamageBuffArea()
    {
        triggerProjectileGO = PoolBase.instance.GetObject(abilityAttackDamageAreaName, this.transform.position);
        triggerProjectileGO.transform.localScale = new Vector2(0f, 0f);
        triggerProjectileGO.transform.DOScale(new Vector2(1f, 1f), abilityAttackDamageAreaScaleTime);
        triggerProjectileGO.transform.DORotate(new Vector3(0, 0, 720f), abilityAttackDamageAreaScaleTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return new WaitForSeconds(abilityAttackDamageAreaScaleTime);
        triggerProjectileGO.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerRadius);
    }
}