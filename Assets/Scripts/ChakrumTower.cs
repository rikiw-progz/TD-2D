using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakrumTower : MonoBehaviour
{
    private List<GameObject> enemyList = new();
    private float fireCountdown = 0f;
    public float fireCooldown = 0.2f;
    private bool canAttack = false;
    public float towerDamage = 10f;
    public int bulletAmount = 1;
    public int bounceAmount = 1;
    public float chakrumSpeed = 5f;
    public float chakrumRadius = 5f;
    public float chakrumRotationSpeed = 30f;

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
            GameObject bulletGO = EnemyPool.instance.GetEnemyObject("Chakrum Bullet", this.transform.position);
            ChakrumBullet chakrumBullet = bulletGO.GetComponent<ChakrumBullet>();

            if (chakrumBullet != null)
            {
                chakrumBullet.target = enemyList[i].transform;
                chakrumBullet.damage = towerDamage;
                chakrumBullet.maxBounces = bounceAmount;
                chakrumBullet._chakrumSpeed = chakrumSpeed;
                chakrumBullet._chakrumRadius = chakrumRadius;
                chakrumBullet._chakrumRotationSpeed = chakrumRotationSpeed;
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