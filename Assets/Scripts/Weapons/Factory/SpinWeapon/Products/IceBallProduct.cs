using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallProduct : MonoBehaviour, IWeaponProduct
{
    [SerializeField]
    private string m_ProductName = "IceBall";
    public string ProductName { get => m_ProductName; set => m_ProductName = value; }

    [SerializeField]
    private float m_RotationSpeed = 30f; // Set the rotation speed for FireBall
    public float RotationSpeed { get => m_RotationSpeed; set => m_RotationSpeed = value; }

    [SerializeField] private int m_DamageValue = 5;
    [SerializeField] bool shouldKnockBack;

    private ParticleSystem m_ParticleSystem;
    
    public void Initialize()
    {
        gameObject.name = m_ProductName;
        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.Stop();
            m_ParticleSystem.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);
            if (collider2D.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_DamageValue, shouldKnockBack);
            }
        }
    }
}