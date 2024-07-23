using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class WeaponManager : MonoBehaviour
{
    public WeaponMappingData weaponMappingData;

    void Start()
    {
        // Example usage
        WeaponProduct selectedProduct = WeaponProduct.FireBall;
        WeaponType weaponType = GetWeaponType(selectedProduct);
        Debug.Log($"Weapon Product: {selectedProduct}, Weapon Type: {weaponType}");
    }

    WeaponType GetWeaponType(WeaponProduct product)
    {
        foreach (var entry in weaponMappingData.mappings)
        {
            if (entry.product == product)
            {
                return entry.type;
            }
        }
        Debug.LogWarning("WeaponProduct not found in mapping.");
        return default;
    }
}

[CreateAssetMenu(fileName = "WeaponMappingData", menuName = "Weapon/WeaponMapping")]
public class WeaponMappingData : ScriptableObject
{
    public List<WeaponMappingEntry> mappings;
}

[System.Serializable]
public class WeaponMappingEntry
{
    public WeaponProduct product;
    public WeaponType type;
}