using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFactory : Health
{
    // Abstract method to get a product instance.
    public abstract IEnemyProduct GetProduct(Vector3 position);

    // Shared method with all factories.
    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}
