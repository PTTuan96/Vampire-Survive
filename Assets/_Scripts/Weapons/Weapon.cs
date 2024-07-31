using System;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class Weapon : MonoBehaviour
{
    [Tooltip("List of weapon factories")]
    [SerializeField] private WeaponFactory[] m_WeaponFactories;

    void Awake()
    {
        InitializeWeaponMappings();
        InitializeWeaponComponentMappings();
    }

    void Update()
    {
        // for (int i = 0; i <= 9; i++)
        // {
        //     KeyCode keyCode = KeyCode.Alpha0 + i;
        //     if (Input.GetKeyDown(keyCode))
        //     {
        //         if(i == 1)
        //         {
        //             TrySpawnWeapon(WeaponProduct.FireBall, true);
        //             // UpdateStats();
        //         }

        //         if(i == 2)
        //         {
        //             TrySpawnWeapon(WeaponProduct.IceBall, true);
        //         }

        //         if(i == 3)
        //         {
        //             TrySpawnWeapon(WeaponProduct.Knife, true);
        //         }

        //         if(i == 4)
        //         {
        //             TrySpawnWeapon(WeaponProduct.Sword, true);
        //         }
        //     }
        // }
    }

    public List<IWeaponProduct> CreateAllWeapons()
    {
        // Get the type of the WeaponType enum
        Type enumType = typeof(WeaponProduct);

        // Get all enum values
        Array enumValues = Enum.GetValues(enumType);

        List<IWeaponProduct> weaponProducts = new(); 

        foreach (WeaponProduct weaponProduct in enumValues)
        {
            WeaponFactory weaponFactory = GetWeaponFactory(weaponProduct);
            IWeaponProduct product = weaponFactory.CreateWeaponProduct(weaponProduct);
            if(product != null)
            {
                weaponProducts.Add(product);
                weaponFactory.SetWeaponLevel(weaponProduct, Level_0);
            }
        }

        return weaponProducts;
    }

    public int GetWeaponLevel(WeaponProduct weaponProduct)
    {
        return GetWeaponFactory(weaponProduct).GetWeaponLevel(weaponProduct);
    }

    public void TrySpawnWeapon(WeaponProduct weaponProduct)
    {
        WeaponFactory weaponFactory = GetWeaponFactory(weaponProduct);
        weaponFactory.SetStatsWeaponEachFactory(weaponProduct);
    }

    public WeaponFactory GetWeaponFactory(WeaponProduct weaponProductParameter)
    {
        if (m_WeaponFactories == null)
        {
            Debug.LogError("m_WeaponFactories is null!");
            return null;
        }
        foreach (var factory in m_WeaponFactories)
        {
            if (factory == null)
            {
                Debug.LogError("Factory is null!");
                continue;
            }

            if(GetWeaponType(weaponProductParameter) == factory.WeaponType)
            {
                return factory;
            }
        }
        return null;
    }
}
