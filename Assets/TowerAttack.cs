using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private List<GameObject> enemyList = new();
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;
    private float fireCountdown = 0f;
    [SerializeField] private float fireCooldown = 1f;
    private bool canAttack = false;
    private int randomNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Remove(collision.gameObject);
            canAttack = false;
        }
    }

    private void Update()
    {
        if(fireCountdown <= 0f && canAttack)
        {
            Shoot();
            fireCountdown = fireCooldown;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)EnemyPool.instance.GetEnemyObject("Bullet", this.transform.position);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            for(int i = 0; i < enemyList.Count; i++)
            {
                randomNumber = Random.Range(0, i);
                bullet.target = enemyList[randomNumber].transform;
            }
        }
    }
}