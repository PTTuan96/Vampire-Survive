using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public class SpinWeaponsFactory : WeaponFactory
{
    // [Tooltip("List of spin weapon prefabs")]
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
                        // productInstance.transform.SetParent(transform, true);

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

    #region Events
    // public delegate void StatsChange(WeaponProduct weaponProduct, Attributes attribute);
    // public event StatsChange OnStatsChange;
    // public UnityEvent<WeaponProduct , Attributes> OnStatsChange;
    #endregion

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        if(Stats.Count > 0)
        {
            int lvl = GetWeaponLevel(weaponProduct);
            if(lvl < 0)
            {
                return;
            }

            lvl++;
            if(weaponProduct == WeaponProduct.LightArea)
            {
                FindComponentsByType(weaponProduct)[0].UpdateStats(0, Stats[lvl]);
            } else {
                if(lvl > Level_1)
                {
                    CreateWeaponProduct(weaponProduct);
                }    
                // OnStatsChange.Invoke(weaponProduct, Stats[lvl]);
                List<IWeaponProduct> weaponProducts = FindComponentsByType(weaponProduct);
                float angleStep = 360f / weaponProducts.Count;
                int i = 1;
                foreach (IWeaponProduct weapon in weaponProducts)
                {
                    // Calculate the angle for this weapon
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    weapon.UpdateStats(
                        angle,
                        Stats[lvl]
                    );
                    i++;
                }
            }
            GetAndUpdateWeaponLevel(weaponProduct, lvl);
        } else
        {
            Debug.Log("Stats.Count = 0 in SetStatsWeaponEachFactory method");
        }
    }
}
