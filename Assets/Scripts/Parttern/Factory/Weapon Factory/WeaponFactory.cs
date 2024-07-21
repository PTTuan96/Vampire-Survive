using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactory : MonoBehaviour
{
    // Abstract method to get a product instance.
    public T GetSpecificWeapon<T>(Vector3 position, List<GameObject> prefabHolders, List<GameObject> prefabs) where T : Component, IWeaponProduct
    {
        foreach (GameObject prefab in prefabs)
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
                    foreach (GameObject holderPrefab in prefabHolders)
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

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}

