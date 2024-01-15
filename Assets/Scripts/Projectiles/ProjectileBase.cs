using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public Transform target;
    public float damage = 10f;
    public float projectileSpeed = 5f;
    public float chancePercentage = 30f;

    public virtual void PerformAction()
    {
        // Implement your action logic here
        Debug.Log("Triggered!");
    }

    private void Update()
    {
        if(Vector2.Distance(this.transform.position, target.transform.position) < 0.1f)
        {
            float randomValue = Random.Range(0f, 100f);

            if (randomValue < chancePercentage)
            {
                // Execute your action here
                PerformAction();
            }
            else
            {
                // Action did not occur
            }

            target.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);
            gameObject.SetActive(false);
        }

        if (target != null && target.gameObject.activeInHierarchy)
        {
            MoveTowardsTarget();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void MoveTowardsTarget()
    {
        float step = projectileSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}