using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
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
            // Deactivate immediately on collision
            // StopCoroutine(DeactivateRoutine(timeoutDelay));

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




    [SerializeField] private Rigidbody2D rb;

    [Tooltip("Projectile force")]
    [SerializeField] private float muzzleVelocity = 1500f;

    private IObjectPool<Knife> objectPool;
    public IObjectPool<Knife> ObjectPool
    {
        get => objectPool;
        set => objectPool = value;
    }

    [SerializeField] private float throwDelay = 0.5f; // Delay before the knife is thrown
    [SerializeField] private float lifeTime = 5f;      // Time before the knife is deactivated

    private bool isThrown = false;

    public void Launch(Vector3 direction)
    {
        if (isThrown) return; // Prevent re-launching if already thrown

        StartCoroutine(LaunchWithDelay(direction));
    }

    private IEnumerator LaunchWithDelay(Vector3 direction)
    {
        isThrown = true;

        yield return new WaitForSeconds(throwDelay);

        // Detach knife from its parent to ensure it doesn’t follow parent’s position
        Transform originalParent = transform.parent;
        transform.SetParent(null);

        // Launch the knife
        StartCoroutine(MoveKnife(direction));

        // Optionally, reattach the knife to its parent if needed
        // transform.SetParent(originalParent);
    }

    private IEnumerator MoveKnife(Vector3 direction)
    {
        Vector3 startPosition = direction;
        float elapsedTime = 0f;

        while (elapsedTime < lifeTime)
        {
            float step =  Time.deltaTime * 10; //  weaponAttribute.speed
            transform.position += direction.normalized * step;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deactivate or return the knife to the pool after the lifetime
        gameObject.SetActive(false); // Or use ObjectPool.Release(this) if using an object pool
    }
}
