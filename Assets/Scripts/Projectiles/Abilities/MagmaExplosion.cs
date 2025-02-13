using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagmaExplosion : MonoBehaviour
{
    public float explosionRadius = 2f;
    private float explosionScalingTime = 0.2f;
    private GameObject triggerMagmaExplosion;

    private void OnEnable()
    {
        this.transform.localScale = new Vector2(0f, 0f);
    }

    public void MagmaExplode(GameObject tower, Vector2 hitPoint, float damage)
    {
        Collider2D[] nearbyUnits = Physics2D.OverlapCircleAll(hitPoint, explosionRadius);

        this.transform.DOScale(new Vector2(1f, 1f), explosionScalingTime);

        foreach (Collider2D collider in nearbyUnits)
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
                StartCoroutine(DamageAndCheckFate(tower, collider.gameObject, damage));
            }
        }

        StartCoroutine(DeActivation());
    }

    private IEnumerator DamageAndCheckFate(GameObject tower, GameObject target, float damage)
    {
        yield return new WaitForSeconds(0.15f);
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            bool isDead = enemyHealth.GetEnemyHP(damage);
            if (isDead)
            {
                tower.GetComponent<MagmaGolem>().IncreaseTowerKillCount();

                triggerMagmaExplosion = PoolBase.instance.GetObject("Magma explosion", target.transform.position);
                triggerMagmaExplosion.GetComponent<MagmaExplosion>().MagmaExplode(tower, target.transform.position, damage);
                isDead = false;
            }
        }
    }

    IEnumerator DeActivation()
    {
        yield return new WaitForSeconds(explosionScalingTime);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}