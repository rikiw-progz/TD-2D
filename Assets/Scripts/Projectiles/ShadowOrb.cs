using UnityEngine;

public class ShadowOrb : TowerBase
{
    public float shadowOrbDurationTime = 3f;
    public float shadowOrbSpeed = 1f;
    public float shadowOrbDamage = 10f;

    private Vector3 randomDirection;

    private void OnEnable()
    {
        // Generate a random direction vector when the object is instantiated
        randomDirection = Random.insideUnitCircle.normalized;
    }

    public override void Update()
    {
        base.Update();

        transform.position += shadowOrbSpeed * Time.deltaTime * randomDirection;

        shadowOrbDurationTime -= Time.deltaTime;

        if (shadowOrbDurationTime <= 0)
            this.gameObject.SetActive(false);
    }

    public override void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void ProjectileTrigger(GameObject target)
    {
        // do nothing
    }
}