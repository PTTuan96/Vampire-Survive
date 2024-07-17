using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }
    public void Initialize();
    void HandleDamageableInterface(MonoBehaviour monoBehaviour);
}
