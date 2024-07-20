using UnityEngine.Pool;
using UnityEngine;

public class EnemyTakeDamage : ObjectTakeDamage, IEnemyAnimation
{
    public ObjectPool<GreenBeeProduct> ObjectPool { get; set; }
    private bool isPooledObject;

    public override void Die()
    {
        if (m_IsDead)
            return;

        base.Die();

        // Custom death logic for enemies
        if (isPooledObject && ObjectPool != null)
        {
            ObjectPool.Release((GreenBeeProduct)this); // Release back to the pool if pooled
        }
        else
        {
            Destroy(gameObject); // Destroy if not pooled
        }
    }

    public void Initialize(bool isPooled = false)
    {
        isPooledObject = isPooled;
        CurrentHealth = MaxHealth;
        m_IsDead = false;
    }
}
