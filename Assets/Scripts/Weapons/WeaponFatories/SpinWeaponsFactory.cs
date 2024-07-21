using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeaponsFactory : WeaponFactory
{
    [Tooltip("List of holder spin weapon prefabs")]
    [SerializeField] private List<GameObject> holderWeaponPrefabs;

    [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    public override T GetSpecificWeapon<T>(Vector3 position)
    {
        foreach (GameObject prefab in spinWeaponPrefabs)
        {
            if (prefab.GetComponent<T>() != null)
            {
                GameObject productInstance = Instantiate(prefab, position, Quaternion.identity);
                T component = productInstance.GetComponent<T>();
                
                // Log the ProductName if it implements IWeaponProduct
                if (component is IWeaponProduct weaponProduct)
                {
                    Debug.Log("Created weapon: " + weaponProduct.ProductName);

                    // Find the corresponding holder by name
                    foreach (GameObject holderPrefab in holderWeaponPrefabs)
                    {
                        if (holderPrefab.name.Equals(weaponProduct.ProductName))
                        {
                            // Set the product instance as a child of the holder
                            productInstance.transform.SetParent(holderPrefab.transform);
                            break;
                        }
                    }
                }
                
                return component;
            }
        }

        Debug.LogWarning("No prefab found with the specified component.");
        return null;
    }
}
