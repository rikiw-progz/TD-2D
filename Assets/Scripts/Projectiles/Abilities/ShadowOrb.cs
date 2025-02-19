using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowOrb : MonoBehaviour
{
    public float shadowOrbDamage = 50f;

    [Header("Fear")]
    [SerializeField] private string triggerProjectileEffectName = "Fear effect";
    [SerializeField] private float slowDebuffAmountPercent = 20f;
    [SerializeField] private float slowDebuffDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().GetEnemyHP(shadowOrbDamage);
            collision.GetComponent<EnemyMove>().ApplyMovementSlow(slowDebuffAmountPercent, slowDebuffDuration, triggerProjectileEffectName);
        }
    }
}