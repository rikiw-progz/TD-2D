using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserLivingVolcano : TowerBase
{
    public GameObject heatAura;
    public float heatAuraRadius = 3f;
    public float heatAuraDamage = 10f;
    private float heatAuraCountdown = 0f;
    private readonly float heatAuraCooldown = 1f; // per second

    private void Start()
    {
        heatAura.GetComponent<RectTransform>().sizeDelta = new Vector2(heatAuraRadius * 250f, heatAuraRadius * 250f);
    }

    public override void Update()
    {
        base.Update();

        HeatAura();
    }

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    private void HeatAura()
    {
        if (heatAuraCountdown <= 0f)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, heatAuraRadius);

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<EnemyHealth>().GetEnemyHP(heatAuraDamage);
                }
            }
            heatAuraCountdown = heatAuraCooldown;
        }
        heatAuraCountdown -= Time.deltaTime;
    }
}