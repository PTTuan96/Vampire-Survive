using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public abstract class WeaponFactory : MonoBehaviour
{
    [SerializeField] public WeaponType weaponType; 
    [SerializeField] protected List<Attributes> stats;
    [SerializeField] protected int weaponLevel;
    protected bool statsUpdated;

    private Coroutine toggleCoroutine;

    [Tooltip("Notifies listeners of updated Stats percentage")]
    public UnityEvent<float> StatsChange; 

    protected List<IWeaponProduct> activeWeapons = new List<IWeaponProduct>();
    protected GameObject productInstance;

    // Abstract method to get a product instance.
    public abstract IWeaponProduct GetSpecificWeapon(Vector3 position, WeaponProduct weaponProduct);
    public abstract void SetStatsWeaponEachFactory(WeaponProduct weaponProduct);

    protected void AddWeapon(string holderName)
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

    protected IWeaponProduct[] GetWeaponProduct(WeaponProduct weaponProduct)
    {
        IWeaponProduct[] weaponProducts;

        // Find the correct weapon product based on the WeaponProduct enum
        switch (weaponProduct)
        {
            case WeaponProduct.FireBall:
                weaponProducts = transform.GetComponentsInChildren<FireBall>();
                break;
            case WeaponProduct.IceBall:
                weaponProducts = transform.GetComponentsInChildren<IceBall>();
                break;
            case WeaponProduct.Knife:
                weaponProducts = transform.GetComponentsInChildren<Knife>();
                break;
            case WeaponProduct.Sword:
                weaponProducts = transform.GetComponentsInChildren<Sword>();
                break;
                
            // Add cases for other weapon types
            default:
                Debug.LogWarning("Unsupported weapon type: " + weaponProduct);
                return new IWeaponProduct[0]; // Return an empty array if the weapon type is unsupported
        }

        // Check if we found any weapon products of the specified type
        if (weaponProducts.Length > 0)
        {
            return weaponProducts; // Return the array of weapon products
        }

        Debug.LogWarning("No weapon of type " + weaponProduct + " found.");
        return new IWeaponProduct[0]; // Return an empty array if no weapon products were found
    }

    protected void StartToggleParentActiveState(float inactiveDuration, float activeDuration, List<IWeaponProduct> weaponProducts)
    {
        // Stop any existing coroutine before starting a new one
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }

        // Update the list of active weapons
        activeWeapons = weaponProducts;

        StartCoroutine(ToggleParentActiveCoroutine(inactiveDuration, activeDuration));
    }

    protected IEnumerator ToggleParentActiveCoroutine(float inactiveDuration, float activeDuration)
    {
        while (true) // Loop indefinitely
        {
            SetChildObjectsActive(true); // Activate the object
            yield return new WaitForSeconds(activeDuration); // Wait for the active duration

            SetChildObjectsActive(false); // Deactivate the object
            yield return new WaitForSeconds(inactiveDuration); // Wait for the inactive duration
        }
    }

    protected void SetChildObjectsActive(bool isActive)
    {
        foreach (IWeaponProduct weaponProduct in activeWeapons)
    {
        GameObject weaponGameObject = (weaponProduct as MonoBehaviour)?.gameObject;
        if (weaponGameObject != null)
        {
            weaponGameObject.SetActive(isActive); // Set the active state of the GameObject
        }
        else
        {
            Debug.LogWarning("IWeaponProduct does not have an associated GameObject.");
        }
    }
    }

    public void LevelUp()
    {
        if(weaponLevel < stats.Count - 1)
        {
            weaponLevel++;

            statsUpdated = true;
        }
    }

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}

