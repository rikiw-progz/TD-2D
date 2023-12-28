using UnityEngine;

public class ChakrumBullet : MonoBehaviour
{
    public float damage = 40f;
    public float _chakrumSpeed = 10f;
    public int maxBounces = 1;
    public float _chakrumRadius = 5f;
    public float _chakrumRotationSpeed = 30f;

    public Transform target;
    private int currentBounces = 0;
    private float step;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);

            currentBounces++;
            if (currentBounces < maxBounces && target != null)
            {
                // Set the next target to the nearest enemy
                SetNextTarget();
            }
            else
            {
                // If max bounces reached or no more targets, deactivate the bullet
                gameObject.SetActive(false);
            }
        }
    }

    private void SetNextTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _chakrumRadius);

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Iterate through each collider to find the nearest enemy
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.transform != target)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }

        target = nearestEnemy;
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            MoveTowardsTarget();
            RotateInfinitely();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void MoveTowardsTarget()
    {
        step = _chakrumSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }

    private void RotateInfinitely()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _chakrumRotationSpeed);
    }
}