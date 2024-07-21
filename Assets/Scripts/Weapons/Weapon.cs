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
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        if (isFireBallsActive)
        {
            TrySpawnWeapon<FireBalls>(spawnPosition);
        }

        if (isIceBallsActive)
        {
            TrySpawnWeapon<IceBalls>(spawnPosition);
        }

        if (isKnifesActive)
        {
            TrySpawnWeapon<Knifes>(spawnPosition);
        }

        if (isSwordsActive)
        {
            TrySpawnWeapon<Swords>(spawnPosition);
        }
    }

    private void TrySpawnWeapon<T>(Vector3 position) where T : Component, IWeaponProduct
    {
        IWeaponProduct weapon = GetWeaponFromFactories<T>(position);

        if (weapon != null)
        {
            Debug.Log($"Weapon spawned: {weapon.ProductName}");
        }
        else
        {
            Debug.LogWarning($"No weapon of type {typeof(T).Name} spawned.");
        }
    }

    private T GetWeaponFromFactories<T>(Vector3 position) where T : Component, IWeaponProduct
    {
        foreach (WeaponFactory factory in m_WeaponFactories)
        {
            T weapon = factory.GetSpecificWeapon<T>(position);
            if (weapon != null)
            {
                return weapon;
            }
        }

        return null;
    }
}
