using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Attributes attributes;

    private void Start()
    {
        // Example of accessing the enemy's attributes
        Debug.Log("Enemy Health: " + attributes.CurrentHealth);
    }

    // Example methods to take damage and heal
    public void ApplyDamage(float damage)
    {
        attributes.TakeDamage(damage);
    }

    public void ApplyHeal(float healAmount)
    {
        attributes.Heal(healAmount);
    }
}
