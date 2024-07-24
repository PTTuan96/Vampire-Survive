using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class SpinWeaponsFactory : WeaponFactory
{
    [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    public override IWeaponProduct CreateWeaponProduct(WeaponProduct weaponProduct)
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

    void Start()
    {
        // uIController.LevelUpButtons[0].UpdateButtonDisplay(this); // this = WeaponFactory
    }

    void Update()
    {
        // if(statsUpdated)
        // {
        //     statsUpdated = false;

        //     foreach (GameObject prefab in spinWeaponPrefabs)
        //     {
        //         // Check if the prefab has an IWeaponProduct component
        //         if (prefab.TryGetComponent<IWeaponProduct>(out var component))
        //         {
        //             CreateWeaponProduct(component.WeaponTypeSelected);
        //             // Update stats for the weapon based on its type
        //             SetStatsWeaponEachFactory(component.WeaponTypeSelected);
        //         }
        //     }
        // }
    }

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        if(Stats.Count > 0)
        {
            IWeaponProduct[] weaponProducts = GetWeaponProduct(weaponProduct);

            if (weaponProducts == null || weaponProducts.Length == 0)
            {
                Debug.LogWarning("No weapons of type " + weaponProduct + " found under the transform.");
                return;
            }

            int weaponLevel = GetWeaponLevel(weaponProduct);

            // Calculate the angle step based on the number of weapons
            float angleStep = 360f / weaponProducts.Length;
            int i = 1;

            foreach (IWeaponProduct weapon in weaponProducts)
            {
                // Calculate the angle for this weapon
                float angle = i * angleStep * Mathf.Deg2Rad;
                weapon.UpdateStats(
                    angle, 
                    Stats[weaponLevel].Damage, 
                    Stats[weaponLevel].Range, 
                    Stats[weaponLevel].Speed
                );
                i++;

                // Example usage: add weapons to the list and start the toggle coroutine
                activeWeapons.Add(weapon);
            }

            // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
            StartToggleParentActiveState(
                Stats[weaponLevel].TimeBetweenAttacks, 
                Stats[weaponLevel].Duration
            );
        } else
        {
            Debug.Log("Stats.Count = 0 in SetStatsWeaponEachFactory method");
        }
    }
}
