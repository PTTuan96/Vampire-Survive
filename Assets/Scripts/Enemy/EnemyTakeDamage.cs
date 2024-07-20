using UnityEngine.Pool;
using UnityEngine;

public class EnemyTakeDamage<T> : ObjectTakeDamage, IEnemyAnimation where T : EnemyTakeDamage<T>
{
    public ObjectPool<T> ObjectPool { get; set; }
    private bool isPooledObject;

    protected override void Die()
    {
        if (m_IsDead)
            return;

        base.Die();

        // Custom death logic for enemies
        if (isPooledObject && ObjectPool != null)
        {
            ObjectPool.Release((T)this); // Release back to the pool if pooled
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

    public float KnockBackSpeed(float moveSpeed)
    {
        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if(moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if(knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * knockBackTime);
            }
        }

        return moveSpeed;
    }
}
