using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float damage = 40f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
        else
            this.gameObject.SetActive(false);
    }
}