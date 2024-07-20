using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactory : MonoBehaviour
{
    // Abstract method to get a product instance.
    public abstract IWeaponProduct GetProduct(Vector3 position);

    // Shared method with all factories.
    public string GetLog(IWeaponProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }

    public void AddWeaponToList(GameObject child, GameObject obj)
    {
        if (child != null)
        {
            // ChildComponent childComponent = child.GetComponent<ChildComponent>();

            // if (childComponent != null)
            // {
            //     childComponent.objectList.Add(obj);
            //     Debug.Log("Added object to child's list: " + obj.name);
            // }
            // else
            // {
            //     Debug.LogError("ChildComponent script not found on child GameObject: " + child.name);
            // }
        }
    }
}
