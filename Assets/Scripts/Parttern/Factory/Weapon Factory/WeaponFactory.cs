using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactory : MonoBehaviour
{
    // Abstract method to get a product instance.
    public abstract T GetSpecificWeapon<T>(Vector3 position) where T : Component, IWeaponProduct;

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}

