using UnityEngine;

public class ObjectTakeDamage : Health, IDamageable
{
    [Tooltip("Customize rate of damage for this target")]
    [SerializeField] private float m_DamageMultiplier = 1f;

    // Overloaded method with knockback
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount * m_DamageMultiplier);
    }

    [SerializeField] protected float knockBackTime = .5f;
    protected float knockBackCounter;

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if(shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
