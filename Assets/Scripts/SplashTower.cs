using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTower : MonoBehaviour
{
    private List<GameObject> enemyList = new();
    private float fireCountdown = 0f;
    public float fireCooldown = 2f;
    private bool canAttack = false;
    public float towerDamage = 10f;
    public int bulletAmount = 1;
    public float bulletSpeed = 5f;
    public float splashRadius = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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
        if (fireCountdown <= 0f && canAttack)
        {
            Shoot();
            fireCountdown = fireCooldown;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        ShuffleEnemyList();

        for (int i = 0; i < Mathf.Min(bulletAmount, enemyList.Count); i++)
        {
            GameObject bulletGO = EnemyPool.instance.GetEnemyObject("Splash Bullet", this.transform.position);
            SplashBullet splashBullet = bulletGO.GetComponent<SplashBullet>();

            if (splashBullet != null)
            {
                splashBullet.target = enemyList[i].transform;
                splashBullet.damage = towerDamage;
                splashBullet._bulletSpeed = bulletSpeed;
                splashBullet._splashRadius = splashRadius;
            }
        }
    }

    void ShuffleEnemyList()
    {
        for (int i = enemyList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = enemyList[i];
            enemyList[i] = enemyList[randomIndex];
            enemyList[randomIndex] = temp;
        }
    }
}