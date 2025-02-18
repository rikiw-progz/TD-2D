using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFireball : MonoBehaviour
{
    public float bounceRadius = 2f; // Radius to check for nearby units
    public int maxBounces = 5; // Maximum number of bounces
    private int bounceCount = 0;
    private HashSet<Collider2D> affectedUnits = new();
    private float projectileSpeed = 5f;
    private float triggerRandomValue;
    public float fireBallBounceChancePercentage = 10f;
    private float closestDistance = Mathf.Infinity;

    public void PerformElectricFireball(GameObject target, float damage, int maxBounceCount)
    {
        // Call method to handle bounce targeting
        maxBounces = maxBounceCount;
        StartCoroutine(ElectricFireballProjectileCoroutine(target, damage));
        affectedUnits.Add(target.GetComponent<Collider2D>());
    }

    private void BounceToNextTarget(Vector2 hitPoint, float damage)
    {
        if (bounceCount < maxBounces)
        {
            Collider2D[] nearbyUnits = Physics2D.OverlapCircleAll(hitPoint, bounceRadius);

            closestDistance = Mathf.Infinity; // Reset closest distance

            Collider2D closestTarget = null;

            foreach (Collider2D collider in nearbyUnits)
            {
                if (collider != null && collider.CompareTag("Enemy") && !affectedUnits.Contains(collider))
                {
                    float distance = Vector2.Distance(hitPoint, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = collider;
                    }
                }
            }

            if (closestTarget != null)
            {
                affectedUnits.Add(closestTarget);
                bounceCount++;
                StartCoroutine(ElectricFireballProjectileCoroutine(closestTarget.gameObject, damage));
            }
            else
            {
                this.gameObject.SetActive(false); // No valid target found
            }
        }
        else
        {
            this.gameObject.SetActive(false); // Max bounces reached
        }
    }

    public virtual IEnumerator ElectricFireballProjectileCoroutine(GameObject target, float damage)
    {
        while (target != null && this.gameObject.activeInHierarchy)
        {
            float step = projectileSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, step);

            if (Vector2.Distance(this.transform.position, target.transform.position) < 0.1f)
            {
                DoDamage(target, damage);
                yield break; // **Prevents extra iterations**
            }

            yield return null;
        }
    }

    private void DoDamage(GameObject target, float damage)
    {
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);

        triggerRandomValue = Random.Range(0f, 100f);

        if (triggerRandomValue < fireBallBounceChancePercentage)
        {
            damage *= 2;
            BounceToNextTarget(this.transform.position, damage);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    // Visualize the bounce radius for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, bounceRadius);
    }

    private void OnDisable()
    {
        affectedUnits.Clear();
        StopAllCoroutines();
        bounceCount = 0;
    }
}