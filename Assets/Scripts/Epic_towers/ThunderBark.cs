using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThunderBark : TowerBase
{
    private int randomTowerIndex;
    private List<Collider2D> towersInRange = new();
    [SerializeField] private string abilityAttackSpeedName;
    [SerializeField] private float attackSpeedPercent = 20f;
    [SerializeField] private float attackSpeedDuration = 5f;
    [SerializeField] private string abilityAreaName;
    [SerializeField] private float abilityAreaScaleTime = 0.3f;
    private float randomNumber;

    [SerializeField] private string triggerStunName = "Stun effect";
    [SerializeField] private float abilityStunDuration = 0.2f;

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

        ApplySpeedBuff(towersInRange[randomTowerIndex].gameObject);
    }

    private void ApplySpeedBuff(GameObject tower)
    {
        if (tower.GetComponent<TowerBase>())
        {
            tower.GetComponent<TowerBase>().ApplyAttackSpeedBuff(attackSpeedPercent, attackSpeedDuration, abilityAttackSpeedName);
            ShockAreaAbility();
        }
    }

    private void ShockAreaAbility()
    {
        randomNumber = Random.Range(0f, 100f);

        if (randomNumber < chancePercentage)
        {
            StartCoroutine(ShockArea());

            foreach (GameObject enemy in enemyList)
            {
                enemy.GetComponent<EnemyMove>().ApplyStun(triggerStunName, abilityStunDuration);
                enemy.GetComponent<EnemyHealth>().GetEnemyHP(abilityDamage);
            }
        }

        towersInRange.Clear();
    }

    IEnumerator ShockArea()
    {
        triggerProjectileGO = PoolBase.instance.GetObject(abilityAreaName, this.transform.position);
        triggerProjectileGO.transform.localScale = new Vector2(0f, 0f);
        triggerProjectileGO.transform.DOScale(new Vector2(1f, 1f), abilityAreaScaleTime);
        triggerProjectileGO.transform.DORotate(new Vector3(0, 0, 720f), abilityAreaScaleTime, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return new WaitForSeconds(abilityAreaScaleTime);
        triggerProjectileGO.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerRadius);
    }
}