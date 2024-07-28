using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class Knife : WeaponProductBase, IWeaponProduct
{
    [SerializeField] private string p_HolderName = "Knifes";
    [SerializeField] private string p_ProductName = "Knife";
    [SerializeField] protected WeaponProduct p_WeaponTypeSelected = WeaponProduct.Knife; // Have to validate this

    public string HolderWeaponName { get => p_HolderName; set => p_HolderName = value; }
    public string ProductWeaponName { get => p_ProductName; set => p_ProductName = value; }
    public WeaponProduct WeaponTypeSelected { get => p_WeaponTypeSelected; set => p_WeaponTypeSelected = value; }
    
    public Sprite SpriteRenderer { get => p_SpriteRenderer; set => p_SpriteRenderer = value; }

    private Attributes weaponAttribute;

    public void Initialize()
    {
        Initialize(ProductWeaponName);
    }

    public bool IsSelectedWeapon(WeaponProduct weaponProduct)
    {
        return weaponProduct == WeaponTypeSelected;
    }


    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);

            // Debug.Log("Damage FireBall: " + p_Damage);
            var damageable = collider2D.GetComponent<IDamageable>();
            damageable?.TakeDamage(weaponAttribute.Damage * s_DamageMultiple);
        }
    }

    public void UpdateStats(float angle, Attributes attribute)
    {
        weaponAttribute = attribute;
        transform.localScale = new Vector3(weaponAttribute.scale, weaponAttribute.scale, weaponAttribute.scale);
    }
}
