using System.Collections;
using UnityEngine;

public class LesserLivingVolcano : TowerBase
{
    public GameObject heatAura;
    public float heatAuraRadius = 3f;
    public float heatAuraDamage = 10f;

    private void Start()
    {
        StartCoroutine(HeatAuraCoroutine());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    IEnumerator HeatAuraCoroutine()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, heatAuraRadius);

        if (!heatAura.activeInHierarchy && this.GetComponent<CircleCollider2D>().enabled == true)
            heatAura.SetActive(true);

        heatAura.GetComponent<RectTransform>().sizeDelta = new Vector2(heatAuraRadius * 250f, heatAuraRadius * 250f);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                DoDamage(collider.gameObject, heatAuraDamage);
            }
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(HeatAuraCoroutine());
    }
}