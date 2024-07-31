using UnityEngine;
using System.Collections;
using static WeaponEnums;
using System;
using UnityEngine.Pool;

public class FireBall : WeaponProductBase, IWeaponProduct
{
    [SerializeField] private string p_HolderName = "FireBalls";
    [SerializeField] private string p_ProductName = "FireBall";
    [SerializeField] protected WeaponProduct p_WeaponTypeSelected = WeaponProduct.FireBall; // Have to validate this

    public string HolderWeaponName { get => p_HolderName; set => p_HolderName = value; }
    public string ProductWeaponName { get => p_ProductName; set => p_ProductName = value; }
    public WeaponProduct WeaponTypeSelected { get => p_WeaponTypeSelected; set => p_WeaponTypeSelected = value; }

    public Sprite SpriteRenderer { get => p_SpriteRenderer; set => p_SpriteRenderer = value; }
    private Attributes weaponAttribute = new();

    private Coroutine orbitCoroutine;

    private float p_CurrentAngle;

    void Update()
    {
        SetOrbit();
    }

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
        p_CurrentAngle = angle;

        // Stop the orbit coroutine
        if (orbitCoroutine != null)
        {
            StopCoroutine(orbitCoroutine);
        }

        // Start the orbit coroutine
        orbitCoroutine = StartCoroutine(SetOrbit());
    }

    public IEnumerator SetOrbit()
    {
        while (true)
        {
            Vector3 parentObject = Utils.GetParentTranform(transform);
            if (parentObject != null)
            {
                // Increment the angle based on the orbit speed and time
                p_CurrentAngle += weaponAttribute.Speed * Time.deltaTime;

                Utils.OrbitMove(p_CurrentAngle, weaponAttribute.Range, out float x, out float y);
                // Set the object's position relative to the orbit center
                transform.position = new Vector3(x, y, 0) + parentObject;
            }
            
            // Wait for the next frame
            yield return null;
        }
    }
}
