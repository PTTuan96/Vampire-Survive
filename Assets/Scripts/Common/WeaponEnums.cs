using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponEnums
{
    public static int Level_1 = 1;
    public static bool SET_ACTIVE = true;
    public static bool SET_DEACTIVE = false;

    public enum WeaponProduct
    {
        FireBall,
        IceBall,
        Knife,
        Sword
    }

    public enum WeaponType
    {
        SpinWeapon,
        ThrowWeapon,
        MeleeWeapon,
        ShotWeapon
    }

    // Dictionary to map WeaponProduct to WeaponType
    private static Dictionary<WeaponProduct, WeaponType> weaponProductToTypeMapping;

    static void Start()
    {
        InitializeWeaponMappings();
        
        // Example usage
        WeaponProduct selectedProduct = WeaponProduct.FireBall;
        WeaponType weaponType = GetWeaponType(selectedProduct);
        Debug.Log($"Weapon Product: {selectedProduct}, Weapon Type: {weaponType}");
    }

    public static void InitializeWeaponMappings()
    {
        weaponProductToTypeMapping = new Dictionary<WeaponProduct, WeaponType>
        {
            { WeaponProduct.FireBall, WeaponType.SpinWeapon },
            { WeaponProduct.IceBall, WeaponType.SpinWeapon },
            { WeaponProduct.Knife, WeaponType.ThrowWeapon },
            { WeaponProduct.Sword, WeaponType.ThrowWeapon }
        };
    }

    // Method to get the WeaponType for a given WeaponProduct
    public static WeaponType GetWeaponType(WeaponProduct product)
    {
        if (weaponProductToTypeMapping.TryGetValue(product, out WeaponType type))
        {
            return type;
        }
        else
        {
            Debug.LogWarning("WeaponProduct not found in mapping.");
            return default;
        }
    }
}
