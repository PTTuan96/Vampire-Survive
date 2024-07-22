using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProduct
{
    string HolderWeaponName { get; set; }
    string ProductWeaponName { get; set; }
    void Initialize(float damage, float range, float speed);

    void UpdateStats(float angle, float damage, float range, float speed);
}
