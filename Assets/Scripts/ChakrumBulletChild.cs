using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakrumBulletChild : MonoBehaviour
{
    private Bullet parentBullet;
    public float chakrumRadius = 5f;
    public Transform nextTarget;

    private void Start()
    {
        parentBullet = GetComponentInParent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            nextTarget = GetNearestEnemyInRadius();

            // Deactivate the bullet after triggering
            parentBullet.gameObject.SetActive(false);
        }
    }

    private Transform GetNearestEnemyInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chakrumRadius);

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Iterate through each collider to find the nearest enemy
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }

        return nearestEnemy;
    }
}