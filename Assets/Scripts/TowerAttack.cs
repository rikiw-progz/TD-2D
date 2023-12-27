using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private List<GameObject> enemyList = new();
    [SerializeField] private GameObject bulletPrefab;
    private float fireCountdown = 0f;
    public float fireCooldown = 0.2f;
    private bool canAttack = false;
    private int randomNumber;
    public float towerDamage;
    public int bulletAmount = 1;

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
        if (enemyList.Count == 1)
        {
            // Single shot for one enemy
            GameObject bulletGO = (GameObject)EnemyPool.instance.GetEnemyObject("Bullet", this.transform.position);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.target = enemyList[0].transform;
                bullet.damage = towerDamage;
            }
        }
        else if (enemyList.Count > 1)
        {
            // Multiple shots for more enemies based on bulletAmount
            for (int i = 0; i < Mathf.Min(bulletAmount, enemyList.Count); i++)
            {
                GameObject bulletGO = (GameObject)EnemyPool.instance.GetEnemyObject("Bullet", this.transform.position);
                Bullet bullet = bulletGO.GetComponent<Bullet>();

                if (bullet != null)
                {
                    randomNumber = Random.Range(0, enemyList.Count);
                    bullet.target = enemyList[randomNumber].transform;
                    bullet.damage = towerDamage;
                }
            }
        }
    }


}