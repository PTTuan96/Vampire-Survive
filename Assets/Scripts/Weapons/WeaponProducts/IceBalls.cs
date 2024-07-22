using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : MonoBehaviour//, IWeaponProduct
{
    // [Tooltip("Why this cannot attached by any Gameobject?")]
    // [SerializeField] private GameObject holderPrefab;

    [Tooltip("This name have to match 100% with the holder gameobject name")]
    [SerializeField] private string m_HolderName = "IceBalls";
    public string HolderWeaponName { get => m_HolderName; set => m_HolderName = value; }
    
    [SerializeField] private string m_ProductName = "IceBall";
    public string ProductWeaponName { get => m_ProductName; set => m_ProductName = value; }

    private float m_damage;
    [SerializeField] private float m_DamageMultiple;
    [SerializeField] private bool shouldKnockBack;

    private ParticleSystem m_ParticleSystem;

    public void Initialize(float damage)
    {
        // Add any unique set up logic here
        gameObject.name = m_ProductName;
        m_damage = damage;

        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (m_ParticleSystem != null)
        {
            m_ParticleSystem.Stop();
            m_ParticleSystem.Play();
        }
    }

    public void UpdateAttack(float damage)
    {
        m_damage = damage * m_DamageMultiple;
    }

    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);

            Debug.Log("Damage FireBall: " + m_damage);
            var damageable = collider2D.GetComponent<IDamageable>();
            damageable?.TakeDamage(m_damage, shouldKnockBack);
        }
    }
}
