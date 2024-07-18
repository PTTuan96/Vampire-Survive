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
            collider2D.GetComponent<EnemyTakeDamage>().TakeDamage(m_DamageValue);
        }
    }

    private void CheckCollisionInterfaces(Collider2D collider2D)
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[1];

        // Get the first contact
        int contactCount = collider2D.GetContacts(contactPoints);

        // Ensure there is at least one contact point
        if (contactCount > 0)
        {
            // Get the first contact point
            ContactPoint2D contactPoint = contactPoints[0];

            // Slight offset to move it outside of the surface
            float pushDistance = 0.1f;
            Vector3 offsetPosition = contactPoint.point + contactPoint.normal * pushDistance;

            var monoBehaviours = collider2D.gameObject.GetComponents<MonoBehaviour>();

            foreach (var monoBehaviour in monoBehaviours)
            {
                // Debug.Log("monoBehaviour: " + monoBehaviour);
                HandleDamageableInterface(monoBehaviour);
                // HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
            }
        }
    }

    public void HandleDamageableInterface(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is IDamageable damageable)
        {
            damageable.TakeDamage(m_DamageValue);
        }
    }
}
