using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    protected DamageNumberController damageNumberController;
    protected ExperienceController expController;

    [SerializeField] private float m_MaxHealth = 100f;
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] bool m_ResetOnStart;

    [Tooltip("Notifies listeners that this object has died")]
    public UnityEvent Died;

    [Tooltip("Notifies listeners of updated health percentage")]
    public UnityEvent<float> HealthChanged;

    protected bool m_IsInvulnerable;
    protected bool m_IsDead;

    public float MaxHealth { get => m_MaxHealth; set => m_MaxHealth = value; }
    public float CurrentHealth { get => m_CurrentHealth; set => m_CurrentHealth = value; }
    public bool IsInvulnerable { get => m_IsInvulnerable; set => m_IsInvulnerable = value; }

    private void Awake()
    {
        damageNumberController = DamageNumberController.Instance;
        expController = ExperienceController.Instance;

        if (m_ResetOnStart)
            m_CurrentHealth = MaxHealth;
    }

    private void Start()
    {
        HealthChanged.Invoke(CurrentHealth / MaxHealth);
        m_CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        // If already dead, do nothing
        if (m_IsDead || m_IsInvulnerable)
            return;

        m_CurrentHealth -= amount;
        
        // Check for death condition.
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            Die();
        }
        
        // Notify listeners of the health change with the current health percentage.
        HealthChanged.Invoke(CurrentHealth / MaxHealth); // Pass the current health percentage

        damageNumberController.SpawnDamage(amount, transform.position);
    }

    public virtual void Heal(float amount)
    {
        // Don't heal if already dead
        if (m_IsDead)
            return;

        m_CurrentHealth += amount;

        if (m_CurrentHealth > MaxHealth)
            m_CurrentHealth = MaxHealth;

        HealthChanged.Invoke(CurrentHealth / MaxHealth);
    }


    // Notify listeners that this object is dead and disable the GameObject to prevent further interaction.
    protected virtual void Die()
    {
        // Only die once
        if (m_IsDead)
            return;
        
        m_IsDead = true;

        // Here the event send when an Oobject die
        Died.Invoke();
        // gameObject.SetActive(false);
    }
}
