using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void TakeDamage(float amount, bool shouldKnockBack);

    // void Heal(float amount);
}
    