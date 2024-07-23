using UnityEngine;
using static WeaponEnums;

public class FireBall : WeaponProductBase, IWeaponProduct
{
    [SerializeField] private string p_HolderName = "FireBalls";
    [SerializeField] private string p_ProductName = "FireBall";
    [SerializeField] protected WeaponProduct p_WeaponTypeSelected = WeaponProduct.FireBall; // Have to validate this
    
    public string HolderWeaponName { get => p_HolderName; set => p_HolderName = value; }
    public string ProductWeaponName { get => p_ProductName; set => p_ProductName = value; }
    public WeaponProduct WeaponTypeSelected { get => p_WeaponTypeSelected; set => p_WeaponTypeSelected = value; }

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
        return weaponProduct == WeaponTypeSelected;
    }

    private void Update()
    {
        SetOrbit();
    }

    // just An exaple for set up function from factory to the product
    // this method can and should be in the in the factory
    private void SetOrbit()
    {
        Vector3 parentObject = Utils.GetParentTranform(transform);
        if (parentObject != null)
        {
            // Increment the angle based on the orbit speed and time
            p_CurrentAngle += p_OrbitSpeed * Time.deltaTime;

            Utils.OrbitMove(p_CurrentAngle, p_OrbitDistance, out float x, out float y);
            // Set the object's position relative to the orbit center
            transform.position = new Vector3(x, y, 0) + parentObject;
        }
    }

    public void UpdateStats(float angle, float damage, float range, float speed)
    {
        p_Damage = damage * s_DamageMultiple;
        
        p_OrbitDistance = range * s_RangeMultiple;

        p_OrbitSpeed = speed * s_SpeedMultiple;;

        // Calculate the angle step based on the number of children
        p_CurrentAngle = angle;

        SetOrbit();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) // is used for trigger collisions one.
    {
        if(collider2D.CompareTag("Enemy"))
        {
            // CheckCollisionInterfaces(collider2D);

            // Debug.Log("Damage FireBall: " + p_Damage);
            var damageable = collider2D.GetComponent<IDamageable>();
            damageable?.TakeDamage(p_Damage, shouldKnockBack);
        }
    }
}
