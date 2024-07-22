using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeaponsFactory : WeaponFactory
{
    [Tooltip("List of throw weapon prefabs")]
    [SerializeField] private List<GameObject> throwWeaponPrefabs;

    public override IWeaponProduct GetSpecificWeapon<T>(Vector3 position)
    {
        foreach (GameObject prefab in throwWeaponPrefabs)
        {
            if (prefab.GetComponent<T>() != null)
            {
                productInstance = Instantiate(prefab, position, Quaternion.identity);
                T component = productInstance.GetComponent<T>();
                if(productInstance != null)
                {
                    CreateHolderOrAddWeapon(component.HolderWeaponName);
                } else
                {
                    Debug.Log("Created weapon with hoder name: " + component.HolderWeaponName);
                }
            }
        }

        return null;
    }
}
