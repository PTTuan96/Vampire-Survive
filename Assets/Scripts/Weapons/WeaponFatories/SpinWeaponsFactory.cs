using System.Collections.Generic;
using UnityEngine;

public class SpinWeaponsFactory : WeaponFactory
{
    [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    public override IWeaponProduct GetSpecificWeapon<T>(Vector3 position)
    {
        foreach (GameObject prefab in spinWeaponPrefabs)
        {
            if (prefab.GetComponent<T>() != null)
            {
                productInstance = Instantiate(prefab, position, Quaternion.identity);
                T component = productInstance.GetComponent<T>();
                
                if(productInstance != null)
                {
                    CreateHolderOrAddWeapon(component.HolderWeaponName);

                    component.Initialize(
                        stats[weaponLevel].damage, 
                        stats[weaponLevel].range, 
                        stats[weaponLevel].speed
                    );
                    
                    SetStatsWeaponEachFactory();
                    return component;
                } else
                {
                    Debug.Log("Created weapon with hoder name: " + component.HolderWeaponName);
                }
            }
        }

        return null;
    }

    void Update()
    {
        // Example usage: deactivate for 3 seconds (timeBetweenAttacks), then reactivate for 2 seconds (duration)
        StartToggleParentActiveState(stats[weaponLevel].timeBetweenAttacks, stats[weaponLevel].duration, transform);
    }

    protected void SetStatsWeaponEachFactory()
    {
        // timeBetweenSpam = stats[weaponLevel].timeBetweenAttacks;

        // damager.lifeTime = stats[weaponLevel].duration;

        // spawnCounter = 0f;

        // StatsChange.Invoke(stats[weaponLevel].damage);

        FireBalls[] fireBalls = transform.GetComponentsInChildren<FireBalls>();
        // Calculate the angle step based on the number of children
        float angleStep = 360f / fireBalls.Length;
        int i = 1;
        foreach (FireBalls fireBall in fireBalls)
        {
            // Calculate the angle for this child -> line -> triangle -> ...
            float angle = i * angleStep * Mathf.Deg2Rad;
            fireBall.UpdateStats(angle, stats[weaponLevel].damage, stats[weaponLevel].range, stats[weaponLevel].speed);
            i++;
        }
    }
}
