using UnityEngine;

public class FireBalls : MonoBehaviour, IWeaponProduct
{
    // [Tooltip("Why this cannot attached by any Gameobject?")]
    // [SerializeField] private GameObject holderPrefab;

    [Tooltip("This name have to match 100% with the holder gameobject name")]
    [SerializeField] private string m_HolderName = "FireBalls";
    public string HolderWeaponName { get => m_HolderName; set => m_HolderName = value; }
    
    [SerializeField] private string m_ProductName = "FireBall";
    public string ProductWeaponName { get => m_ProductName; set => m_ProductName = value; }

    [SerializeField] private float s_DamageMultiple;
    [SerializeField] private float s_RangeMultiple;
    [SerializeField] private float s_SpeedMultiple;
    [SerializeField] private bool shouldKnockBack;
    private float p_Damage;
    private float p_OrbitDistance;
    private float p_OrbitSpeed;

    private float p_CurrentAngle;
    private ParticleSystem p_ParticleSystem;

    public void Initialize(float damage, float range, float speed)
    {
        // Add any unique set up logic here
        gameObject.name = m_ProductName;

        p_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (p_ParticleSystem != null)
        {
            p_ParticleSystem.Stop();
            p_ParticleSystem.Play();
        }
    }

    private void Update()
    {
        SetOrbit();
    }

    // just An exaple for set up function from factory to the product
    // this method can and should be in the in the factory
    private void SetOrbit()
    {
        Vector3 parentObject = Utils.GetParentTranform(transform);
        if (parentObject != null)
        {
            // Increment the angle based on the orbit speed and time
            p_CurrentAngle += p_OrbitSpeed * Time.deltaTime;

            Utils.OrbitMove(p_CurrentAngle, p_OrbitDistance, out float x, out float y);
            // Set the object's position relative to the orbit center
            transform.position = new Vector3(x, y, 0) + parentObject;
        }
    }

    public void UpdateStats(float angle, float damage, float range, float speed)
    {
        p_Damage = damage * s_DamageMultiple;
        
        p_OrbitDistance = range * s_RangeMultiple;

        p_OrbitSpeed = speed * s_SpeedMultiple;;

        // Calculate the angle step based on the number of children
        p_CurrentAngle = angle;

        SetOrbit();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);

            // Debug.Log("Damage FireBall: " + p_Damage);
            var damageable = collider2D.GetComponent<IDamageable>();
            damageable?.TakeDamage(p_Damage, shouldKnockBack);
        }
    }
}
