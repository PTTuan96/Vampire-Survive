using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : ObjectTakeDamage, IEnemyAnimation
{
    [Tooltip("Effect to instantiate on explosion")]
    // [SerializeField] GameObject m_ExplosionPrefab;

    protected override void Die()
    {
        base.Die();
        // DieAnimation();
    }
}
