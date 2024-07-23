using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public interface IWeaponProduct
{
    string HolderWeaponName { get; set; }
    string ProductWeaponName { get; set; }
    void Initialize();

    void UpdateStats(float angle, float damage, float range, float speed);

    bool IsSelectedWeapon(WeaponProduct weaponProduct);
}
