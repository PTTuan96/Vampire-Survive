using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SlimeProduct //: EnemyTakeDamage, IEnemyProduct //, IDamageable
{
    [SerializeField]
    private string m_ProductName = "Slime";
    public string ProductName { get => m_ProductName; set => m_ProductName = value; }
    
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    private ParticleSystem m_ParticleSystem;

    [SerializeField] private float moveSpeed;
    
    private Transform target;

    public ObjectPool<SlimeProduct> ObjectPool { get; set; }
    
    public void Initialize(Transform playerTransform = null, bool isPooled = false)
    {
        // gameObject.name = m_ProductName;
        // m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        // if (m_ParticleSystem != null)
        // {
        //     m_ParticleSystem.Stop();
        //     m_ParticleSystem.Play();
        // }

        // if (m_Rigidbody != null)
        // {
        //     m_Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        // }

        // target = playerTransform;
        // isPooledObject = isPooled;
        // m_CurrentHealth = m_MaxHealth;
        // m_IsDead = false;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        if (target != null && m_Rigidbody != null)
        {
            // m_Rigidbody.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        // CheckCollisionInterfaces(collision2D);
    }

    public void Defend()
    {
        Debug.Log("Slime defends.");
    }


    // private void OnDisable()
    // {
    //     // Only release to pool if it was pooled and not already dead
    //     if (isPooledObject && !m_IsDead && ObjectPool != null)
    //     {
    //         ObjectPool.Release(this);
    //     }
    // }
}