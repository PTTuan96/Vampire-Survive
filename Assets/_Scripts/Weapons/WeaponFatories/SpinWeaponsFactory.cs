using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public class SpinWeaponsFactory : WeaponFactory
{
    // [Tooltip("List of spin weapon prefabs")]
    [SerializeField] private List<GameObject> spinWeaponPrefabs;

    #region Events
    // public delegate void StatsChange(WeaponProduct weaponProduct, Attributes attribute);
    // public event StatsChange OnStatsChange;
    // public UnityEvent<WeaponProduct , Attributes> OnStatsChange;
    #endregion

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        if(Stats.Count > 0)
        {
            int lvl = GetWeaponLevel(weaponProduct);
            if(lvl < 0)
            {
                return;
            }

            lvl++;
            if(weaponProduct == WeaponProduct.LightArea)
            {
                FindComponentsByType(weaponProduct)[0].UpdateStats(0, Stats[lvl]);
            } else {
                if(lvl > Level_1)
                {
                    CreateWeaponProduct(weaponProduct);
                }    
                // OnStatsChange.Invoke(weaponProduct, Stats[lvl]);
                List<IWeaponProduct> weaponProducts = FindComponentsByType(weaponProduct);
                float angleStep = 360f / weaponProducts.Count;
                int i = 1;
                foreach (IWeaponProduct weapon in weaponProducts)
                {
                    // Calculate the angle for this weapon
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    weapon.UpdateStats(
                        angle,
                        Stats[lvl]
                    );
                    i++;
                }
            }
            GetAndUpdateWeaponLevel(weaponProduct, lvl);
        } else
        {
            Debug.Log("Stats.Count = 0 in SetStatsWeaponEachFactory method");
        }
    }
}
