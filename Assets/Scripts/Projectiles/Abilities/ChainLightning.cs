using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public float bounceRadius = 50f; // Radius to check for nearby units
    public int maxBounces = 3; // Maximum number of bounces
    public float bounceForce = 10f; // How strong the bounce force is
    private int bounceCount = 0; // Number of bounces done so far
    private GameObject projectileGO;
    private HashSet<Collider2D> affectedUnits = new HashSet<Collider2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Perform bounce logic if we haven't exceeded max bounces
        if (bounceCount < maxBounces)
        {
            // Get the position of the impact
            Vector2 hitPoint = collision.contacts[0].point;

            // Call method to handle bounce targeting
            //BounceToNextTarget(hitPoint);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void PerformChainLightning(float damage)
    {
        if (bounceCount < maxBounces)
        {
            Debug.Log(0);

            // Call method to handle bounce targeting
            BounceToNextTarget(this.transform.position, damage);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void BounceToNextTarget(Vector2 hitPoint, float damage)
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
        Debug.Log(1);
        // If a target is found, apply bounce force to it
        if (nearestTarget != null)
        {
            affectedUnits.Add(nearestTarget);

            projectileGO = PoolBase.instance.GetObject("Electric line renderer", this.transform.position);
            StartCoroutine(AbilityLineRendererProjectileCoroutine(projectileGO, nearestTarget.gameObject, damage));

            // Increment bounce count
            bounceCount++;
        }
    }

    public virtual IEnumerator AbilityLineRendererProjectileCoroutine(GameObject go, GameObject target, float abilityDamage)
    {
        Debug.Log(2);
        if (target != null && go.activeInHierarchy)
        {
            go.GetComponent<CustomLineRenderer>().CustomSetUpLine(this.transform.position, target.transform.position);
            DoDamage(target, abilityDamage);

            yield return new WaitForSeconds(0.15f);
            go.SetActive(false);

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    private void DoDamage(GameObject target, float damage)
    {
        Debug.Log(3);
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
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
    }
}