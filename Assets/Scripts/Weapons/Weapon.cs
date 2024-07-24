using System;
using UnityEngine;
using static WeaponEnums;

public class Weapon : MonoBehaviour
{
    [Tooltip("List of weapon factories")]
    [SerializeField] private WeaponFactory[] m_WeaponFactories;

    private WeaponFactory weaponFactory;

    [SerializeField] private bool isFireBallsActive;
    [SerializeField] private bool isIceBallsActive;
    [SerializeField] private bool isKnifesActive;
    [SerializeField] private bool isSwordsActive;

    void Awake()
    {
        InitializeWeaponMappings();
        CreateAllWeapons();
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

    private void CreateAllWeapons()
    {
        // Get the type of the WeaponType enum
        Type enumType = typeof(WeaponType);

        // Get all enum values
        Array enumValues = Enum.GetValues(enumType);

        foreach (WeaponProduct weaponProduct in enumValues)
        {
            foreach(WeaponFactory weaponFactory in m_WeaponFactories)
            {
                IWeaponProduct product = weaponFactory.CreateWeaponProduct(weaponProduct);
                if(product != null)
                {
                    weaponFactory.SetWeaponLevel(weaponProduct, Level_1);
                }
            }
        }
    }

    public void SetActiveWeapon(WeaponProduct weaponProduct, bool set)
    {
        GetWeaponFactory(weaponProduct).SetActiveWeapon(weaponProduct, set);
    }

    public int GetWeaponLevel(WeaponProduct weaponProduct)
    {
        return GetWeaponFactory(weaponProduct).GetWeaponLevel(weaponProduct);
    }

    public void TrySpawnWeapon(WeaponProduct weaponProductParameter, bool isUpdate)
    {
        weaponFactory = GetWeaponFactory(weaponProductParameter);
        if(!isUpdate)
            weaponFactory.CreateWeaponProduct(weaponProductParameter);
        else
            weaponFactory.SetStatsWeaponEachFactory(weaponProductParameter);
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

    public IWeaponProduct GetWeaponInfo(WeaponProduct weaponProduct)
    {
        return GetWeaponFactory(weaponProduct).GetWeaponInfo(weaponProduct);
    }
}
