using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShadowOrb : MonoBehaviour
{
    public readonly List<GameObject> enemyList = new();

    [Header("Bullet")]
    public int projectileAmount = 1;
    public string projectileName = "Name of your bullet here";
    public bool projectileFinishEffect = false;

    [Header("Tower stats")]
    public float projectileSpeed = 5f;
    [HideInInspector] public float fireCountdown = 0f;
    public float fireCooldown = 1f;
    public float towerDamage = 10f;
    public float towerRadius = 100f;
    public float chancePercentage = 30f;

    private Vector3 randomDirection;

    public float shadowOrbDurationTime = 3f;
    public float shadowOrbSpeed = 1f;
    public float shadowOrbDamage = 10f;

    private void OnEnable()
    {
        // Generate a random direction vector when the object is instantiated
        randomDirection = Random.insideUnitCircle.normalized;
    }

    public void Update()
    {
        if (fireCountdown <= 0f)
        {
            GetEnemiesInRange();
            Shoot();
            fireCountdown = fireCooldown;
        }
        fireCountdown -= Time.deltaTime;

        transform.position += shadowOrbSpeed * Time.deltaTime * randomDirection;

        shadowOrbDurationTime -= Time.deltaTime;

        if (shadowOrbDurationTime <= 0)
            this.gameObject.SetActive(false);
    }

    private void GetEnemiesInRange()
    {
        enemyList.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, towerRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                enemyList.Add(colliders[i].gameObject);
            }
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < Mathf.Min(projectileAmount, enemyList.Count); i++)
        {
            GameObject projectileGO = PoolBase.instance.GetObject(projectileName, this.transform.localPosition);
            StartCoroutine(ProjectileCoroutine(projectileGO, enemyList[i]));
        }
    }

    public virtual IEnumerator ProjectileCoroutine(GameObject go, GameObject target)
    {
        while (target != null && go.activeInHierarchy)
        {
            float step = projectileSpeed * Time.deltaTime;
            go.transform.position = Vector2.MoveTowards(go.transform.position, target.transform.position, step);

            if (Vector2.Distance(go.transform.position, target.transform.position) < 0.1f)
            {
                float randomValue = Random.Range(0f, 100f);

                if (randomValue < chancePercentage)
                {
                    // Execute your action here
                }
                else
                {
                    // Action did not occur
                }

                DoDamage(target, towerDamage);

                go.SetActive(false);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public void DoDamage(GameObject target, float damage)
    {
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
        if (target.activeInHierarchy)
        {
            GameObject textDamageGO = PoolBase.instance.GetObject("Damage text", target.transform.localPosition);
            textDamageGO.GetComponent<TextMeshProUGUI>().text = this.towerDamage.ToString();
            StartCoroutine(TextDamageDeactivation(textDamageGO));
        }
    }

    IEnumerator TextDamageDeactivation(GameObject textGO)
    {
        yield return new WaitForSeconds(0.1f);
        textGO.SetActive(false);
    }
}