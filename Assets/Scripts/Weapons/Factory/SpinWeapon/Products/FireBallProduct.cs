using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProduct : MonoBehaviour, IWeaponProduct
{
    [SerializeField]
    private string m_ProductName = "FireBall";
    public string ProductName { get => m_ProductName; set => m_ProductName = value; }

    [SerializeField]
    private float m_RotationSpeed = 30f; // Set the rotation speed for FireBall
    public float RotationSpeed { get => m_RotationSpeed; set => m_RotationSpeed = value; }

    [SerializeField] private int m_DamageValue = 5;

    private ParticleSystem m_ParticleSystem;

    public void Initialize()
    {
        // Add any unique set up logic here
        gameObject.name = m_ProductName;

        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.Stop();
            m_ParticleSystem.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);
            // Attempt to get the IDamageable component from the collider
            if (collider2D.TryGetComponent<IDamageable>(out var damageable))
            {
                // Apply damage
                damageable.TakeDamage(m_DamageValue);
            }
        }
    }
}
