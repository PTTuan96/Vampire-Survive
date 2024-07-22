using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactory : MonoBehaviour
{
    protected GameObject productInstance;

    // Abstract method to get a product instance.
    public abstract IWeaponProduct GetSpecificWeapon<T>(Vector3 position) where T : Component, IWeaponProduct;

    protected void CreateHolderOrAddWeapon(string holderName)
    {
        bool exists = Utils.GetChildNames(transform).Contains(holderName);
        GameObject holderObject;
        if (!exists)
        {
            holderObject = Utils.CreateAndAttachGameObject(transform, holderName);
        }
        else
        {
            holderObject = Utils.GetChildByName(transform, holderName);
        }

        productInstance.transform.SetParent(holderObject.transform, true);
    }

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}

