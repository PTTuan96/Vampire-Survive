using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponEnums
{
    public static int Level_0 = 0;
    public static int Level_1 = 1;
    public static bool SET_ACTIVE = true;
    public static bool SET_DEACTIVE = false;
    public static bool IS_UPDATE = true;
    public static bool IS_CREATE = false;

    public enum WeaponProduct
    {
        FireBall,
        IceBall,
        Knife,
        Sword, 
        LightArea
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
    private static Dictionary<WeaponProduct, Type> weaponProductToComponentMapping;
    public static Dictionary<WeaponProduct, (GameObject prefab, Type componentType)> weaponProductMappings;

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
            { WeaponProduct.Sword, WeaponType.ThrowWeapon },
            { WeaponProduct.LightArea, WeaponType.SpinWeapon }
        };
    }

    public static void InitializeWeaponComponentMappings(List<GameObject> prefabs)
    {
        // Initialize the dictionary with enum-to-(prefab, component type) mappings
        weaponProductMappings = new Dictionary<WeaponProduct, (GameObject, System.Type)>
        {
            { WeaponProduct.FireBall, (prefabs.Find(p => p.GetComponent<FireBall>() != null), typeof(FireBall)) },
            { WeaponProduct.IceBall, (prefabs.Find(p => p.GetComponent<IceBall>() != null), typeof(IceBall)) },
            { WeaponProduct.Knife, (prefabs.Find(p => p.GetComponent<Knife>() != null), typeof(Knife)) },
            { WeaponProduct.Sword, (prefabs.Find(p => p.GetComponent<Sword>() != null), typeof(Sword)) },
            { WeaponProduct.LightArea, (prefabs.Find(p => p.GetComponent<LightingArea>() != null), typeof(LightingArea)) }
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

    public static void InitializeWeaponComponentMappings()
    {
        // Initialize the dictionary with enum-to-component mappings
        weaponProductToComponentMapping = new Dictionary<WeaponProduct, System.Type>
        {
            { WeaponProduct.FireBall, typeof(FireBall) },
            { WeaponProduct.IceBall, typeof(IceBall) },
            { WeaponProduct.Knife, typeof(Knife) },
            { WeaponProduct.Sword, typeof(Sword) },
            { WeaponProduct.LightArea, typeof(LightingArea) }
        };
    }

    // Method to find all objects with a specific component type
    public static List<IWeaponProduct> FindComponentsByType(WeaponProduct type)
    {
        List<IWeaponProduct> componentsList = new List<IWeaponProduct>();

        // Check if the type exists in the dictionary
        if (weaponProductToComponentMapping.TryGetValue(type, out Type componentType))
        {
            MonoBehaviour[] componentsArray = (MonoBehaviour[])FindObjectsOfType(componentType);
            foreach (var component in componentsArray)
            {
                if (component is IWeaponProduct weaponProductComponent)
                {
                    componentsList.Add(weaponProductComponent);
                }
            }
        }
        else
        {
            Debug.LogWarning($"Component type {type} not found in the dictionary.");
        }

        return componentsList;
    }

    // Wrapper for FindObjectsOfType to use reflection
    private static Array FindObjectsOfType(Type type)
    {
        return UnityEngine.Object.FindObjectsOfType(type);
    }
}
