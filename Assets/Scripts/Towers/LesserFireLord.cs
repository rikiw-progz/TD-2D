using UnityEngine;

public class LesserFireLord : TowerBase
{
    public float fireLordLiquidFireDamage = 5f;
    public float fireLordLiquidFireDuration = 5f;
    public string fireLordDebuffName = "Firelord debuff name";

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileTrigger(GameObject target)
    {
        if (target.GetComponent<EnemyHealth>().damageOverTimeDuration == true)
            return;

        target.GetComponent<EnemyHealth>().Debuff(fireLordLiquidFireDamage, fireLordLiquidFireDuration, fireLordDebuffName);
    }
}