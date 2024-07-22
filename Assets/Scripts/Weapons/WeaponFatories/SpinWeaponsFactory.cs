using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeaponsFactory : WeaponFactory
{
    [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    public override IWeaponProduct GetSpecificWeapon<T>(Vector3 position)
    {
        foreach (GameObject prefab in spinWeaponPrefabs)
        {
            if (prefab.GetComponent<T>() != null)
            {
                productInstance = Instantiate(prefab, position, Quaternion.identity);
                T component = productInstance.GetComponent<T>();
                if(productInstance != null)
                {
                    CreateHolderOrAddWeapon(component.HolderWeaponName);
                    return component;
                } else
                {
                    Debug.Log("Created weapon with hoder name: " + component.HolderWeaponName);
                }
            }
        }

        return null;
    }
}
