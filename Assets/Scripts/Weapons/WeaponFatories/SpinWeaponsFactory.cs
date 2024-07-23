using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class SpinWeaponsFactory : WeaponFactory
{
    [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    public override IWeaponProduct GetSpecificWeapon(Vector3 position, WeaponProduct weaponProduct)
    {
        // Iterate through all weapon prefabs
        foreach (GameObject prefab in spinWeaponPrefabs)
        {
            // Check if the prefab has an IWeaponProduct component
            if (prefab.TryGetComponent<IWeaponProduct>(out var component))
            {
                // Check if the component matches the specified weaponProduct
                if (component.IsSelectedWeapon(weaponProduct))
                {
                    // Instantiate the prefab at the specified position
                    productInstance = Instantiate(prefab, position, Quaternion.identity);
                    
                    // Check if the instantiated product has an IWeaponProduct component
                    if (productInstance.TryGetComponent<IWeaponProduct>(out var createdProduct))
                    {
                        // Add the weapon to the factory's collection (if applicable)
                        AddWeapon(createdProduct.HolderWeaponName);

                        // Initialize the created weapon product
                        createdProduct.Initialize();

                        // Update stats for the weapon based on its type
                        SetStatsWeaponEachFactory(weaponProduct);

                        // Return the created weapon product
                        return createdProduct;
                    }
                }
            }
        }

        // Return null if no matching weapon was found
        return null;
    }

    void Update()
    {
        // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
        // StartToggleParentActiveState(stats[weaponLevel].timeBetweenAttacks, stats[weaponLevel].duration, transform);
    }

    protected void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        IWeaponProduct[] weaponProducts = GetWeaponProduct(weaponProduct);

        if (weaponProducts == null || weaponProducts.Length == 0)
        {
            Debug.LogWarning("No weapons of type " + weaponProduct + " found under the transform.");
            return;
        }

        // Calculate the angle step based on the number of weapons
        float angleStep = 360f / weaponProducts.Length;
        int i = 1;

        foreach (IWeaponProduct weapon in weaponProducts)
        {
            // Calculate the angle for this weapon
            float angle = i * angleStep * Mathf.Deg2Rad;
            weapon.UpdateStats(
                angle, 
                stats[weaponLevel].damage, 
                stats[weaponLevel].range, 
                stats[weaponLevel].speed
            );
            i++;
        }
    }
}
