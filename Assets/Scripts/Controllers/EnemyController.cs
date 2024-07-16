using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody;
    // private IObjectPool<Projectile> m_ObjectPool;
    public float moveSpeed;
    private Transform target;

    [SerializeField] private int m_DamageValue = 5;
    [SerializeField] private float m_Lifetime = 3f;
    public float hitWaitTime = 1f;
    private float hitCounter;

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        if (m_Rigidbody != null)
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    void Update()
    {
        m_Rigidbody.velocity = (target.position - transform.position).normalized * moveSpeed;

        if(hitCounter > 0f)
        {   
            hitCounter -= Time.deltaTime;
        }
    }


    // OnCollision 3 event
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Debug.Log("Collision started with " + collision2D.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if(hitCounter <= 0f)
        {
            CheckCollisionInterfaces(collision2D);
            // DeactivateProjectile();
            hitCounter = hitWaitTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("Collision ended with " + collision.gameObject.name);
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
            Debug.Log("monoBehaviour: " + monoBehaviour);
            HandleDamageableInterface(monoBehaviour);
            // HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
        }
    }

    private void HandleDamageableInterface(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is IDamageable damageable)
        {
            damageable.TakeDamage(m_DamageValue);
            hitCounter = hitWaitTime;
        }
    }

    private void HandleEffectTriggerInterface(MonoBehaviour monoBehaviour, Vector3 position)
    {
        if (monoBehaviour is IEffectTrigger effectTrigger)
        {
            effectTrigger.TriggerEffect(position);
        }
    }

    private void DeactivateProjectile()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = 0f;

        // m_ObjectPool.Release(this);
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(m_Lifetime);
        DeactivateProjectile();
    }
}
