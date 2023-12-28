using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float damage = 40f;
    public float bulletSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
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
        float step = bulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}
