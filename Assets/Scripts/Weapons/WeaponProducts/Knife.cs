using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class Knife : WeaponProductBase, IWeaponProduct
{
    [SerializeField] private string p_HolderName = "Knifes";
    [SerializeField] private string p_ProductName = "Knife";
    public string HolderWeaponName { get => p_HolderName; set => p_HolderName = value; }
    public string ProductWeaponName { get => p_ProductName; set => p_ProductName = value; }

    private float p_Damage;
    private float p_OrbitDistance;
    private float p_OrbitSpeed;

    private float p_CurrentAngle;
    
    public void Initialize()
    {
        Initialize(ProductWeaponName);
    }

    public bool IsSelectedWeapon(WeaponProduct weaponProduct)
    {
        return weaponProduct == weaponTypeSelected;
    }


    public void UpdateStats(float angle, float damage, float range, float speed)
    {
        p_Damage = damage * s_DamageMultiple;
        
        p_OrbitDistance = range * s_RangeMultiple;

        p_OrbitSpeed = speed * s_SpeedMultiple;;

        // Calculate the angle step based on the number of children
        p_CurrentAngle = angle;
    }

    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);

            Debug.Log("Damage FireBall: " + p_Damage);
            var damageable = collider2D.GetComponent<IDamageable>();
            damageable?.TakeDamage(p_Damage, shouldKnockBack);
        }
    }
}