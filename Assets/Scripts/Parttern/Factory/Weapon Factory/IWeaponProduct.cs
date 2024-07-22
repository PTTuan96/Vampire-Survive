using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProduct
{
    string HolderWeaponName { get; set; }
    // GameObject HolderPrefab { get; set; }
    void Attack();
}
