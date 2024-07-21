using UnityEngine.Pool;
using UnityEngine;

public class EnemyTakeDamage<T> : ObjectTakeDamage, IEnemyAnimation where T : EnemyTakeDamage<T>
{
    [SerializeField] protected int expToGive; 
    
    public ObjectPool<T> ObjectPool { get; set; }
    private bool isPooledObject;
    

    protected override void Die()
    {
        if (m_IsDead)
            return;

        base.Die();
        
        expController.SpawnExp(transform.position, expToGive);

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

    protected void CheckCollisionInterfaces(Collision2D collision2D, float m_DamageValue)
    {
        var monoBehaviours = collision2D.gameObject.GetComponents<MonoBehaviour>();
        foreach (var monoBehaviour in monoBehaviours)
        {
            HandleDamageableInterface(monoBehaviour, m_DamageValue);
        }
    }

    protected void HandleDamageableInterface(MonoBehaviour monoBehaviour, float m_DamageValue)
    {
        // Check if the MonoBehaviour is both IDamageable and Player
        if (monoBehaviour is IDamageable damageable && monoBehaviour.GetComponent<Player>() != null)
        {
            // Debug.Log($"{monoBehaviour.GetType().Name} implements IDamageable and is a Player");
            damageable.TakeDamage(m_DamageValue);
        }
        else
        {
            // Debug.Log($"{monoBehaviour.GetType().Name} does not implement IDamageable or is not a Player");
        }
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
