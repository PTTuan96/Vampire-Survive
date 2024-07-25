using System.Collections.Generic;
using System.Linq;
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

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        if(Stats.Count > 0)
        {
            int lvl = GetWeaponLevel(weaponProduct) + 1;
            // Debug.Log("lvl at SetStatsWeaponEachFactory " + lvl);
            IWeaponProduct[] weaponProducts = GetWeaponProduct(weaponProduct);
            
            Debug.Log("GetWeaponLevel(weaponProduct):  " + GetWeaponLevel(weaponProduct));
            Debug.Log("Stats[lvl].Amount:  " + Stats.Count);
            

            if(GetWeaponLevel(weaponProduct) < Stats.Count )
            {
                foreach (var weapon in activeWeapons.Where(w => w.WeaponTypeSelected == weaponProduct).ToList())
                {
                    activeWeapons.Remove(weapon);
                }
            }
            
            Debug.Log("activeWeapons.Count:  " + activeWeapons.Count);

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
                if (activeWeapons.Contains(weapon))
                {
                    activeWeapons.Remove(weapon);
                }
                // Calculate the angle for this weapon
                float angle = i * angleStep * Mathf.Deg2Rad;
                weapon.UpdateStats(
                    angle, 
                    Stats[lvl].Damage, 
                    Stats[lvl].Range, 
                    Stats[lvl].Speed
                );
                i++;

                // Example usage: add weapons to the list and start the toggle coroutine
                activeWeapons.Add(weapon);
            }

            // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
            StartToggleParentActiveState(
                Stats[lvl].TimeBetweenAttacks, 
                Stats[lvl].Duration
            );
            GetAndUpdateWeaponLevel(weaponProduct, lvl);
        } else
        {
            Debug.Log("Stats.Count = 0 in SetStatsWeaponEachFactory method");
        }
    }
}
