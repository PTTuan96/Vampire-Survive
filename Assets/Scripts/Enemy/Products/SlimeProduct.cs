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
    [SerializeField] private float m_DamageValue = 5;
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

            //Knock back function
            moveSpeed = KnockBackSpeed(moveSpeed);
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (hitCounter <= 0f)
        {
            CheckCollisionInterfaces(collision2D, m_DamageValue);
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

    public void Defend()
    {
        Debug.Log("Slime defends.");
    }
}