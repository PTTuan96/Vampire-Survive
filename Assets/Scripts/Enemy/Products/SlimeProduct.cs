using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SlimeProduct : EnemyTakeDamage<SlimeProduct>, IEnemyProduct
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

    private Transform target;
    private float hitCounter;

    public void Initialize(Transform playerTransform = null, bool isPooled = false)
    {
        base.Initialize(isPooled); // Call base Initialize
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
            moveSpeed = KnockBackSpeed(moveSpeed);
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (hitCounter <= 0f)
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
        var monoBehaviours = collision2D.gameObject.GetComponents<MonoBehaviour>();
        foreach (var monoBehaviour in monoBehaviours)
        {
            HandleDamageableInterface(monoBehaviour);
        }
    }

    public void HandleDamageableInterface(MonoBehaviour monoBehaviour)
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

    public void Defend()
    {
        Debug.Log("Slime defends.");
    }
}