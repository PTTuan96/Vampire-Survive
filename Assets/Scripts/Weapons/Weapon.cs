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
            TrySpawnWeapon(WeaponProduct.FireBall);
        }
        if (isIceBallsActive)
        {
            TrySpawnWeapon(WeaponProduct.IceBall);
        }
        if (isKnifesActive)
        {
            TrySpawnWeapon(WeaponProduct.Knife);
        }
        if (isSwordsActive)
        {
            TrySpawnWeapon(WeaponProduct.Sword);
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
                    TrySpawnWeapon(WeaponProduct.FireBall);
                    // UpdateStats();
                }

                if(i == 2)
                {
                    TrySpawnWeapon(WeaponProduct.IceBall);
                }

                if(i == 3)
                {
                    TrySpawnWeapon(WeaponProduct.Knife);
                }

                if(i == 4)
                {
                    TrySpawnWeapon(WeaponProduct.Sword);
                }
            }
        }
    }

    private void TrySpawnWeapon(WeaponProduct weaponProductParameter)
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
                factory.GetSpecificWeapon(transform.position, weaponProductParameter);
            }
        }
    }
}
