using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(50f);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (target.gameObject.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, target.position, 2f * Time.deltaTime);
        else
            this.gameObject.SetActive(false);
    }
}