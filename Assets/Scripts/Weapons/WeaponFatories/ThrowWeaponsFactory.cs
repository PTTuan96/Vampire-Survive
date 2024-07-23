using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class ThrowWeaponsFactory : WeaponFactory
{
    [Tooltip("List of throw weapon prefabs")]
    [SerializeField] private List<GameObject> throwWeaponPrefabs;

    public override IWeaponProduct GetSpecificWeapon(Vector3 position, WeaponProduct weaponProduct)
    {
        foreach (GameObject prefab in throwWeaponPrefabs)
        {
            IWeaponProduct component = prefab.GetComponent<IWeaponProduct>();
            if (component != null)
            {
                GameObject productInstance = Instantiate(prefab, position, Quaternion.identity);
                IWeaponProduct createdProduct = productInstance.GetComponent<IWeaponProduct>();
                
                if (createdProduct != null)
                {
                    if (createdProduct.IsSelectedWeapon(weaponProduct)) // Use the general method
                    {
                        AddWeapon(createdProduct.HolderWeaponName);

                        createdProduct.Initialize();

                        // SetStatsWeaponEachFactory();
                        return createdProduct;
                    }
                }
            }
        }

        return null;
    }

    void Update()
    {
        // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
        // StartToggleParentActiveState(stats[weaponLevel].timeBetweenAttacks, stats[weaponLevel].duration, transform);
    }
}
