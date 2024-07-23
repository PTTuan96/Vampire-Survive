using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class Weapon : MonoBehaviour
{
    [Tooltip("List of weapon factories")]
    [SerializeField] private WeaponFactory[] m_WeaponFactories;

    [SerializeField] private bool isFireBallsActive;
    [SerializeField] private bool isIceBallsActive;
    [SerializeField] private bool isKnifesActive;
    [SerializeField] private bool isSwordsActive;

    void Start()
    {
        InitializeWeaponMappings();
        if (isFireBallsActive)
        {
            TrySpawnWeapon(WeaponProduct.FireBall, false);
            TrySpawnWeapon(WeaponProduct.FireBall, true);
        }
        if (isIceBallsActive)
        {
            TrySpawnWeapon(WeaponProduct.IceBall, false);
            TrySpawnWeapon(WeaponProduct.IceBall, true);
        }
        if (isKnifesActive)
        {
            TrySpawnWeapon(WeaponProduct.Knife, false);
        }
        if (isSwordsActive)
        {
            TrySpawnWeapon(WeaponProduct.Sword, false);
        }
    }

    void Update()
    {
        for (int i = 0; i <= 9; i++)
        {
            KeyCode keyCode = KeyCode.Alpha0 + i;
            if (Input.GetKeyDown(keyCode))
            {
                if(i == 1)
                {
                    TrySpawnWeapon(WeaponProduct.FireBall, true);
                    // UpdateStats();
                }

                if(i == 2)
                {
                    TrySpawnWeapon(WeaponProduct.IceBall, true);
                }

                if(i == 3)
                {
                    TrySpawnWeapon(WeaponProduct.Knife, true);
                }

                if(i == 4)
                {
                    TrySpawnWeapon(WeaponProduct.Sword, true);
                }
            }
        }
    }

    private void TrySpawnWeapon(WeaponProduct weaponProductParameter, bool isUpdate)
    {
        if (m_WeaponFactories == null)
        {
            Debug.LogError("m_WeaponFactories is null!");
            return;
        }
        foreach (var factory in m_WeaponFactories)
        {
            if (factory == null)
            {
                Debug.LogError("Factory is null!");
                continue;
            }

            if(GetWeaponType(weaponProductParameter) == factory.weaponType)
            {
                if(!isUpdate)
                    factory.GetSpecificWeapon(transform.position, weaponProductParameter);
                else
                    factory.SetStatsWeaponEachFactory(weaponProductParameter);
            }
        }
    }
}
