using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour
{
    public float damage = 40f;
    private float piercingBulletDuration = 5f;

    public void GetTarget(Transform target)
    {
        if (target == null)
            this.gameObject.SetActive(false);

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
       transform.position += 3f * Time.deltaTime * transform.up;

        piercingBulletDuration -= Time.deltaTime;

        if(piercingBulletDuration <= 0)
            this.gameObject.SetActive(false);
    }
}
