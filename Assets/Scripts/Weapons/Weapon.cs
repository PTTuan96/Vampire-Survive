using System.Collections.Generic;
using UnityEngine;

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
        if (isFireBallsActive)
        {
            TrySpawnWeapon<FireBalls>(transform.position);
        }

        if (isIceBallsActive)
        {
            TrySpawnWeapon<IceBalls>(transform.position);
        }

        if (isKnifesActive)
        {
            TrySpawnWeapon<Knifes>(transform.position);
        }

        if (isSwordsActive)
        {
            TrySpawnWeapon<Swords>(transform.position);
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
                    TrySpawnWeapon<FireBalls>(transform.position);
                }

                if(i == 2)
                {
                    TrySpawnWeapon<IceBalls>(transform.position);
                }

                if(i == 3)
                {
                    TrySpawnWeapon<Knifes>(transform.position);
                }

                if(i == 4)
                {
                    TrySpawnWeapon<Swords>(transform.position);
                }
            }
        }
    }

    private void TrySpawnWeapon<T>(Vector3 position) where T : MonoBehaviour, IWeaponProduct
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

            IWeaponProduct weapon = factory.GetSpecificWeapon<T>(position);
            if (weapon != null)
            {
                Debug.Log($"Spawned weapon of type {typeof(T)} at position {position}");
                break;
            }
            else
            {
                Debug.LogWarning($"Failed to get weapon of type {typeof(T)} from factory. Need to set holder Name in prefabs weapon product");
            }
        }
    }
}
