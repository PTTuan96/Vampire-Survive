using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class ThrowWeaponsFactory : WeaponFactory
{
    [Tooltip("List of throw weapon prefabs")]
    [SerializeField] private List<GameObject> throwWeaponPrefabs;

    public override IWeaponProduct CreateWeaponProduct(WeaponProduct weaponProduct)
    {
        // Iterate through all weapon prefabs
        foreach (GameObject prefab in throwWeaponPrefabs)
        {
            // Check if the prefab has an IWeaponProduct component
            if (prefab.TryGetComponent<IWeaponProduct>(out var component))
            {
                // Check if the component matches the specified weaponProduct
                if (component.IsSelectedWeapon(weaponProduct))
                {
                    // Instantiate the prefab at the specified position
                    GameObject productInstance = Instantiate(prefab, transform.position, Quaternion.identity);
                    
                    // Check if the instantiated product has an IWeaponProduct component
                    if (productInstance.TryGetComponent<IWeaponProduct>(out var createdProduct))
                    {
                        // Add the weapon to the factory's collection (if applicable)
                        AddWeapon(createdProduct.HolderWeaponName, productInstance);

                        // Initialize the created weapon product
                        createdProduct.Initialize();
                        
                        // Return the created weapon product
                        return createdProduct;
                    }
                }
            }
        }

        // Return null if no matching weapon was found
        return null;
    }

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        
    }

    void Update()
    {
        // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
        // StartToggleParentActiveState(stats[weaponLevel].timeBetweenAttacks, stats[weaponLevel].duration, transform);
    }
}
