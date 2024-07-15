using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    public Attributes attributes;
    [SerializeField] private float maxHealth = 100f;
    public Slider healthSlider;

    private void Start()
    {
        // Example of accessing the player's attributes
        // maxHealth = 100f; // Set the maximum health
        attributes.CurrentHealth = maxHealth; // Initialize current health to max health
        healthSlider.maxValue = maxHealth;
        healthSlider.value = attributes.CurrentHealth;
    }

    // Example methods to take damage and heal
    public void ApplyDamage(float damage)
    {   
        attributes.TakeDamage(damage);
        healthSlider.value = attributes.CurrentHealth;
    }

    public void ApplyHeal(float healAmount)
    {
        attributes.Heal(healAmount);
    }
}
