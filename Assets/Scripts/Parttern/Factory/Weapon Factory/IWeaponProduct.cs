using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProduct
{
    string ProductName { get; set; }
    // GameObject HolderPrefab { get; set; }
    void Attack();
}
