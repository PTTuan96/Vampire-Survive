using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class LightingArea : WeaponProductBase, IWeaponProduct
{
    [SerializeField] private string p_HolderName = "LightArea";
    [SerializeField] private string p_ProductName = "LightArea";
    [SerializeField] protected WeaponProduct p_WeaponTypeSelected = WeaponProduct.LightArea; // Have to validate this

    public string HolderWeaponName { get => p_HolderName; set => p_HolderName = value; }
    public string ProductWeaponName { get => p_ProductName; set => p_ProductName = value; }
    public WeaponProduct WeaponTypeSelected { get => p_WeaponTypeSelected; set => p_WeaponTypeSelected = value; }
    
    public Sprite SpriteRenderer { get => p_SpriteRenderer; set => p_SpriteRenderer = value; }

    private Attributes weaponAttribute;

    private List<IDamageable> enemies = new List<IDamageable>();

    private float damageCounter;
    private float timeBetweenDamage = 1f; // Example value, set your desired time between damage

    private float hitCounter;
    private float hitCooldown = 1f; // Set your desired cooldown duration here

    public void Initialize()
    {
        Initialize(ProductWeaponName);
    }

    public bool IsSelectedWeapon(WeaponProduct weaponProduct)
    {
        return weaponProduct == WeaponTypeSelected;;
    }

    void Update()
    {
        damageCounter -= Time.deltaTime;
        if (damageCounter <= 0)
        {
            damageCounter = timeBetweenDamage;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].TakeDamage(weaponAttribute.Damage * s_DamageMultiple);
                }
                else
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Add(collision.GetComponent<IDamageable>());

            if (hitCounter <= 0f)
            {
                hitCounter = hitCooldown;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemies.Remove(collision.GetComponent<IDamageable>());
        }
    }


    public void UpdateStats(float angle, Attributes attribute)
    {
        weaponAttribute = attribute;
        transform.localScale = new Vector3(weaponAttribute.scale, weaponAttribute.scale, weaponAttribute.scale);
    }
}
    
