using System.Collections;
using UnityEngine;

public class GreenBeeProduct : MonoBehaviour, IEnemyProduct
{
    [SerializeField]
    private string m_ProductName = "GreenBee";
    public string ProductName { get => m_ProductName; set => m_ProductName = value; }
    
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    private ParticleSystem m_ParticleSystem;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int m_DamageValue = 5;
    [SerializeField] private float m_Lifetime = 3f;
    [SerializeField] private float hitWaitTime = 1f;
    private Transform target;
    private float hitCounter;

    public void Initialize(Transform playerTransform = null)
    {
        // Add any unique set up logic here
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
        // Assume playerTransform is set
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        // Ensure target is set before using it
        if (target != null && m_Rigidbody != null)
        {
            m_Rigidbody.velocity = (target.position - transform.position).normalized * moveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D) // It detects continuous contact between colliders.
    {
        if(hitCounter <= 0f)
        {
            CheckCollisionInterfaces(collision2D);
            // DeactivateProjectile();
            hitCounter = hitWaitTime;
            // Ensure the GameObject is active before starting the coroutine
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DecrementHitCounter());
            }
        }
    }

    private IEnumerator DecrementHitCounter() // Replace for Update()
    {
        while (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }

    private void CheckCollisionInterfaces(Collision2D collision2D)
    {
        // Get the first contact
        ContactPoint2D contactPoint = collision2D.GetContact(0);

        // Slight offset to move it outside of the surface
        float pushDistance = 0.1f;
        Vector3 offsetPosition = contactPoint.point + contactPoint.normal * pushDistance;

        var monoBehaviours = collision2D.gameObject.GetComponents<MonoBehaviour>();
        
        foreach (var monoBehaviour in monoBehaviours)
        {
            // Debug.Log("monoBehaviour: " + monoBehaviour);
            HandleDamageableInterface(monoBehaviour);
            // HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
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
        Debug.Log("Green Bee defends  .");
    }
}

