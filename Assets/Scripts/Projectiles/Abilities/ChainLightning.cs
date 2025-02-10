using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float bounceRadius = 50f; // Radius to check for nearby units
    public int maxBounces = 3; // Maximum number of bounces
    private int bounceCount = 0; // Number of bounces done so far
    private GameObject projectileGO;
    private HashSet<Collider2D> affectedUnits = new HashSet<Collider2D>();
    [SerializeField] private string lineRendererName = "Electric line renderer";

    public void PerformChainLightning(GameObject target, GameObject lineRenderer, float damage, int maxBounceCount)
    {
        // Call method to handle bounce targeting
        maxBounces = maxBounceCount;
        StartCoroutine(DoDamage(target, lineRenderer, damage));
        affectedUnits.Add(target.GetComponent<Collider2D>());
    }

    private void BounceToNextTarget(Vector2 hitPoint, float damage)
    {
        if (bounceCount < maxBounces)
        {
            // Find all colliders within the bounce radius around the impact point
            Collider2D[] nearbyUnits = Physics2D.OverlapCircleAll(hitPoint, bounceRadius);

            // Filter out the projectile itself (to avoid hitting itself)
            Collider2D nearestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D collider in nearbyUnits)
            {
                if (collider != null && collider.CompareTag("Enemy") && !affectedUnits.Contains(collider)) // Make sure it's an enemy
                {
                    // Calculate the distance to each nearby unit
                    float distance = Vector2.Distance(hitPoint, collider.transform.position);
                    // Check if this unit is the closest one
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestTarget = collider;
                    }
                }
            }

            // If a target is found, apply bounce force to it
            if (nearestTarget != null)
            {
                affectedUnits.Add(nearestTarget);
                projectileGO = PoolBase.instance.GetObject(lineRendererName, this.transform.position);
                
                AbilityLineRendererProjectileCoroutine(projectileGO, nearestTarget.gameObject, damage);

                // Increment bounce count
                bounceCount++;
            }
            else
            {
                this.gameObject.SetActive(false);
            }

        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public virtual void AbilityLineRendererProjectileCoroutine(GameObject go, GameObject target, float abilityDamage)
    {
        if (target != null && go.activeInHierarchy)
        {
            go.GetComponent<CustomLineRenderer>().CustomSetUpLine(this.transform.position, target.transform.position);
            StartCoroutine(DoDamage(target, go, abilityDamage));
        }
        else
        {
            go.SetActive(false);
        }
    }

    private IEnumerator DoDamage(GameObject target, GameObject lineRenderer, float damage)
    {
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
        yield return new WaitForSeconds(0.1f);
        lineRenderer.SetActive(false);
        this.transform.position = target.transform.position;
        damage /= 2;
        BounceToNextTarget(this.transform.position, damage);
    }

    // Optional: Visualize the bounce radius for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bounceRadius);
    }

    private void OnDisable()
    {
        affectedUnits.Clear();
        StopAllCoroutines();
        bounceCount = 0;
    }
}