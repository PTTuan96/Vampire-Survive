using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Health, IDamageable
{
    [Tooltip("Customize rate of damage for this target")]
    [SerializeField] float m_DamageMultiplier = 1f;
    public override void TakeDamage(float amount)
    {
        // if(Collider2D == ...)
        // {
        //     m_DamageMultiplier = 0 ...
        // }

        // m_DamageMultiplier = 0 -> No Damage

        base.TakeDamage(amount * m_DamageMultiplier);
        
        // Customize any additional class-specific logic here
        // Debug.Log($"Target custom TakeDamage: {amount}");
    }
}
