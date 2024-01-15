using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    public readonly List<GameObject> enemyList = new();
    private bool canAttack = false;

    [Header("Bullet")]
    public int projectileAmount = 1;
    public string projectileName = "Name of your bullet here";
    public bool projectileFinishEffect = false;

    [Header("Tower stats")]
    public float projectileSpeed = 5f;
    private float fireCountdown = 0f;
    public float fireCooldown = 1f;
    public float towerDamage = 10f;
    public float towerRadius = 100f;
    public float chancePercentage = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Remove(collision.gameObject);
            canAttack = false;
        }
    }

    public virtual void Update()
    {
        if (fireCountdown <= 0f && canAttack)
        {
            Shoot();
            fireCountdown = fireCooldown;
        }
        fireCountdown -= Time.deltaTime;
    }

    public virtual void Shoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < chancePercentage)
        {
            // Execute your action here
            TowerTrigger();
        }
        else
        {
            // Action did not occur
        }
    }

    public virtual void TowerTrigger()
    {
        // Implement your action logic here
        //Debug.Log("Triggered!");
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
                    ProjectileTrigger(target);
                }
                else
                {
                    // Action did not occur
                }

                if(projectileFinishEffect)
                    ProjectileFinish(target);

                target.GetComponent<EnemyHealth>().GetEnemyHP(towerDamage);
                go.SetActive(false);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public virtual void ProjectileTrigger(GameObject target)
    {

    }

    public virtual void ProjectileFinish(GameObject target)
    {

    }

    // Adds targerRadius amount to the radius
    public void ChangeTowerRadius(float radiusAmount)
    {
        GetComponent<CircleCollider2D>().radius += radiusAmount;
        towerRadius = GetComponent<CircleCollider2D>().radius;
    }

    public void ChangeAttackSpeed(float speedAmount)
    {
        // Convert speedAmount to a percentage (e.g., 10.0 will become 0.1)
        float reductionPercentage = speedAmount / 100f;

        // Calculate the amount to reduce fireCooldown by percentage
        float reductionAmount = fireCooldown * reductionPercentage;

        // Apply the reduction to fireCooldown
        fireCooldown -= reductionAmount;
    }
}