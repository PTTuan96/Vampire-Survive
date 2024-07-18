using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SlimeProduct : MonoBehaviour, IEnemyProduct, IDamageable
{
    [SerializeField]
    private string m_ProductName = "Slime";
    public string ProductName { get => m_ProductName; set => m_ProductName = value; }
    
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    private ParticleSystem m_ParticleSystem;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int m_DamageValue = 5;
    [SerializeField] private float hitWaitTime = 1f;
    
    private float m_MaxHealth = 30;
    private float m_CurrentHealth;
    private bool m_IsDead;
    private Transform target;
    private float hitCounter;

    public ObjectPool<SlimeProduct> ObjectPool { get; set; }
    private bool isPooledObject;

    public void Initialize(Transform playerTransform = null, bool isPooled = false)
    {
        gameObject.name = m_ProductName;
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.Stop();
            m_ParticleSystem.Play();
        }

        if (m_Rigidbody != null)
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        target = playerTransform;
        isPooledObject = isPooled;
        m_CurrentHealth = m_MaxHealth;
        m_IsDead = false;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        if (target != null && m_Rigidbody != null)
        {
            m_Rigidbody.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if(hitCounter <= 0f)
        {
            CheckCollisionInterfaces(collision2D);
            hitCounter = hitWaitTime;
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DecrementHitCounter());
            }
        }
    }

    private IEnumerator DecrementHitCounter()
    {
        while (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
            yield return null;
        }
    }

    private void CheckCollisionInterfaces(Collision2D collision2D)
    {
        ContactPoint2D contactPoint = collision2D.GetContact(0);
        float pushDistance = 0.1f;
        Vector3 offsetPosition = contactPoint.point + contactPoint.normal * pushDistance;

        var monoBehaviours = collision2D.gameObject.GetComponents<MonoBehaviour>();
        
        foreach (var monoBehaviour in monoBehaviours)
        {
            HandleDamageableInterface(monoBehaviour);
        }
    }

    public void HandleDamageableInterface(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is IDamageable damageable)
        {
            damageable.TakeDamage(m_DamageValue);
        }
    }

    public void Defend()
    {
        Debug.Log("Slime defends.");
    }

    public void TakeDamage(float amount)
    {
        if (m_IsDead)
            return;

        m_CurrentHealth -= amount;

        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die dont know why");
        if (m_IsDead)
            return;

        m_IsDead = true;
        gameObject.SetActive(false); // Or any other logic you want for death
        
        if (isPooledObject && ObjectPool != null)
        {
            ObjectPool.Release(this); // Release back to the pool if pooled
        }
        else
        {
            Destroy(gameObject); // Destroy if not pooled
        }
    }

    private void OnDisable()
    {
        // Only release to pool if it was pooled and not already dead
        if (isPooledObject && !m_IsDead && ObjectPool != null)
        {
            ObjectPool.Release(this);
        }
    }
}