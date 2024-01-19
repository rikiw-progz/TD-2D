using UnityEngine;

public class LesserBonk : TowerBase
{
    public float bonkStunDuration = 0.5f;

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileTrigger(GameObject target)
    {
        target.GetComponent<EnemyMove>().Stun(bonkStunDuration);
    }
}
