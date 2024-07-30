using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static WeaponEnums;

public class Knife : WeaponProductBase, IWeaponProduct, IPooledWeapon
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
            // Deactivate immediately on collision
            StopCoroutine(DeactivateRoutine(timeoutDelay));
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            // Release the projectile back to the pool
            objectPool?.Release(this);

            var damageable = collider2D.GetComponent<IDamageable>();
            // damageable?.TakeDamage(weaponAttribute.Damage * s_DamageMultiple);
            // Destroy(gameObject);
        }
    }

    public void UpdateStats(float angle, Attributes attribute)
    {
        weaponAttribute = attribute;
        transform.localScale = new Vector3(weaponAttribute.scale, weaponAttribute.scale, weaponAttribute.scale);
    }





    [SerializeField] private float timeoutDelay = 3f;

    public IObjectPool<IPooledWeapon> Pool { get; set; }

    [SerializeField] private Rigidbody2D rb;

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    private void OnEnable()
    {
        // Start the timeout coroutine when the knife is enabled
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the moving Rigidbody
        rb = GetComponent<Rigidbody2D>();
        // rBody.linearVelocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = 0f;

        // Check if objectPool is not null before releasing
        // Release the projectile back to the pool
        objectPool?.Release(this);
    }

    public void Launch(Vector3 direction)
    {
        rb.velocity = direction * 10f; //weaponAttribute.Speed;
    }
}
