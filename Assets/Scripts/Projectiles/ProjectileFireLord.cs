using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireLord : ProjectileBase
{
    public float fireLordLiquidFireDamage = 5f;
    public float fireLordLiquidFireDuration = 5f;
    public string fireLordDebuffName = "Debuff name";

    public override void PerformAction()
    {
        if (target.GetComponent<EnemyHealth>().damageOverTimeDuration == true)
            return;

        target.GetComponent<EnemyHealth>().Debuff(fireLordLiquidFireDamage, fireLordLiquidFireDuration, fireLordDebuffName);
    }
}