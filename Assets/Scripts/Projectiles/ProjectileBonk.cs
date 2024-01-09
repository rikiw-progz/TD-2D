using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBonk : ProjectileBase
{
    public float bonkStunDuration = 0.5f;
    public override void PerformAction()
    {
        target.GetComponent<EnemyMove>().Stun(bonkStunDuration);
    }
}