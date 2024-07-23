using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public abstract class WeaponFactory : MonoBehaviour
{
    [SerializeField] protected List<Attributes> stats;
    [SerializeField] protected int weaponLevel;
    [SerializeField] public WeaponType weaponType; 

    [Tooltip("Notifies listeners of updated Stats percentage")]
    public UnityEvent<float> StatsChange; 

    protected GameObject productInstance;

    // Abstract method to get a product instance.
    public abstract IWeaponProduct GetSpecificWeapon(Vector3 position, WeaponProduct weaponProduct);

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

    protected void StartToggleParentActiveState(float inactiveDuration, float activeDuration, Transform transform)
    {

        List<GameObject> holders = Utils.GetAllChildren(transform.gameObject);
        foreach(GameObject holder in holders)
            {
                if (holders != null)
            {
                StartCoroutine(ToggleParentActiveCoroutine(inactiveDuration, activeDuration, holder));
            }
            else
            {
                Debug.LogWarning("m_ProductParent GameObject is not assigned.");
            }
        }
        
    }

    private IEnumerator ToggleParentActiveCoroutine(float inactiveDuration, float activeDuration, GameObject holder)
    {
        while (true) // Loop indefinitely
        {
            SetChildObjectsActive(true, holder); // Activate the object
            yield return new WaitForSeconds(activeDuration); // Wait for the active duration

            SetChildObjectsActive(false, holder); // Deactivate the object
            yield return new WaitForSeconds(inactiveDuration); // Wait for the inactive duration
        }
    }

    void SetChildObjectsActive(bool isActive, GameObject holder)
    {
        foreach (GameObject child in Utils.GetAllChildren(holder))
        {
            child.SetActive(isActive);
        }
    }

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}

