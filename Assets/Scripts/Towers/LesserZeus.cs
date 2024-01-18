using System.Collections;
using UnityEngine;

public class LesserZeus : TowerBase
{
    public float zeusStunDuration = 0.5f;
    private CircleCollider2D circleCollider;
    public string zeusThunderSplashName = "Zeus thunder splash";
    public int thunderAmount = 0;
    public float _splashRadius = 5f;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    public override void Shoot()
    {
        base.Shoot();

        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetEnemyObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public override void TowerTrigger()
    {
        if (circleCollider == null)
        {
            Debug.LogError("CircleCollider2D not found!");
        }
        else
        {
            StartCoroutine(ThunderStrike());
        }
    }

    IEnumerator ThunderStrike()
    {
        for (int i = 0; i < thunderAmount; i++)
        {
            Vector2 randomPoint = GetRandomPointInCircle();

            GameObject thunderGO = PoolBase.instance.GetEnemyObject(zeusThunderSplashName, randomPoint);
            SetSplashEffectSize(thunderGO);
            SplashAttack(thunderGO);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void SetSplashEffectSize(GameObject go)
    {
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(_splashRadius * 250f, _splashRadius * 250f);
    }

    private void SplashAttack(GameObject thunder)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(thunder.transform.position, _splashRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().GetEnemyHP(towerDamage);
                collider.GetComponent<EnemyMove>().Stun(zeusStunDuration);
            }
        }
        StartCoroutine(SplashEffectDisable(thunder));
    }

    IEnumerator SplashEffectDisable(GameObject thunder)
    {
        yield return new WaitForSeconds(0.15f);
        thunder.SetActive(false);
    }

    private Vector2 GetRandomPointInCircle()
    {
        // Generate a random angle within the circle (in radians)
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate the random point within the circle's radius
        float radius = circleCollider.radius;
        float x = transform.position.x + radius * Mathf.Cos(randomAngle);
        float y = transform.position.y + radius * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }
}