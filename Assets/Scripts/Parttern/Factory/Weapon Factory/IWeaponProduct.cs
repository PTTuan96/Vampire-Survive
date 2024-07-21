using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProduct
{
    public string ProductName { get; set; }
    public GameObject HolderPrefab { get; set; }
    void Attack();
}
