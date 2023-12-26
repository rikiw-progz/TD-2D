using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour
{
    public Transform target;
    public float damage = 40f;
    private float piercingBulletDuration = 5f;

    private void Start()
    {
        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);
        }
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
            transform.position += 3f * Time.deltaTime * transform.up;
        else
            this.gameObject.SetActive(false);

        piercingBulletDuration -= Time.deltaTime;

        if(piercingBulletDuration <= 0)
            this.gameObject.SetActive(false);
    }
}
