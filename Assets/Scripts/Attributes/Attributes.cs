using UnityEngine;

[System.Serializable]
public class Attributes : IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float mana;

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public float Mana
    {
        get => mana;
        set => mana = value;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // Debug.Log("Player Health: " + currentHealth);
        if (currentHealth < 0) 
        {
            currentHealth = 0;
            Die();   
        }
        Debug.Log("Took damage, current health: " + currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Healed, current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // gameObject.SetActive(false);
        // Add additional logic for player death (e.g., respawn, game over, etc.)
    }
}
