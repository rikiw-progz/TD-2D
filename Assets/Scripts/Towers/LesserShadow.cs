using UnityEngine;

public class LesserShadow : TowerBase
{
    public string shadowOrbName = "Shadow orb name";
    public int shadowOrbAmount = 1;
    public float shadowOrbDamage = 10f;
    public float shadowOrbDurationTime = 5f;

    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileTrigger(GameObject target)
    {
        // do nothing
    }

    public override void TowerTrigger()
    {
        for (int i = 0; i < shadowOrbAmount; i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(shadowOrbName, this.transform.localPosition);
            ShadowOrb projectile = projectileGO.GetComponent<ShadowOrb>();

            projectile.shadowOrbDurationTime = shadowOrbDurationTime;
            projectile.shadowOrbDamage = shadowOrbDamage;
        }
    }
}