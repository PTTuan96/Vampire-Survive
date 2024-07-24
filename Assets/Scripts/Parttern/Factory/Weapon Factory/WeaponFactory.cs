using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public abstract class WeaponFactory : MonoBehaviour
{
    protected UIController uIController;

    [SerializeField] public WeaponType WeaponType; 
    [SerializeField] public List<Attributes> Stats;
    [SerializeField] public int StatsLevel; // use for initiate List Stats
    
    // [SerializeField] public Sprite[] Icons;

    protected bool statsUpdated;

    private Coroutine toggleCoroutine;

    [Tooltip("Notifies listeners of updated Stats percentage")]
    public UnityEvent<float> StatsChange; 

    protected List<IWeaponProduct> activeWeapons = new();

    // Create a dictionary to store WeaponType and its corresponding level
    protected Dictionary<WeaponProduct, int> weaponLevels = new();

    private void Awake()
    {
        uIController = UIController.Instance;
        // Initialize the dictionary
        // weaponLevels = new Dictionary<WeaponProduct, int>
        // {
        //     { WeaponProduct.FireBall, 1 },
        //     { WeaponProduct.IceBall, 1 },
        //     { WeaponProduct.Knife, 1 },
        //     { WeaponProduct.Sword, 1 }
        // };
    }

    // Abstract method to get a product instance.
    public abstract IWeaponProduct CreateWeaponProduct(WeaponProduct weaponProduct);

    public abstract void SetStatsWeaponEachFactory(WeaponProduct weaponProduct);

    public int GetWeaponLevel(WeaponProduct weaponProduct)
    {
        if (weaponLevels.TryGetValue(weaponProduct, out int level))
        {
            return level;
        }
        else
        {
            Debug.LogWarning("WeaponType not found in dictionary: " + weaponProduct);
            return -1; // Return -1 or any other default value to indicate not found
        }
    }
   
    // Method to set or update the level of a specific WeaponType
    public void SetWeaponLevel(WeaponProduct weaponProduct, int level)
    {
        // Update the level or add a new entry if it doesn't exist
        if (weaponLevels.ContainsKey(weaponProduct))
        {
            weaponLevels[weaponProduct] = level;
        }
        else
        {
            weaponLevels.Add(weaponProduct, level);
        }
    }

    // Method to get and update the level of a specific WeaponType
    public void GetAndUpdateWeaponLevel(WeaponProduct weaponProduct, int newLevel)
    {
        if (weaponLevels.TryGetValue(weaponProduct, out int currentLevel))
        {
            // Print or use the current level if needed
            Debug.Log("Current Level of " + weaponProduct + ": " + currentLevel);

            // Update the level
            weaponLevels[weaponProduct] = newLevel;

            // Print or use the updated level if needed
            Debug.Log("Updated Level of " + weaponProduct + ": " + newLevel);
        }
        else
        {
            Debug.LogWarning("WeaponType not found in dictionary: " + weaponProduct);
        }
    }

    protected void AddWeapon(string holderName, GameObject productInstance)
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

    public void SetActiveWeapon(WeaponProduct weaponProduct, bool isActive)
    {
        foreach(IWeaponProduct weapon in GetWeaponProduct(weaponProduct))
        {
            GameObject weaponGameObject = (weapon as MonoBehaviour)?.gameObject;
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

    protected void StartToggleParentActiveState(float inactiveDuration, float activeDuration)
    {
        // Stop any existing coroutine before starting a new one
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
        }

        // Update the list of active weapons
        // activeWeapons = weaponProducts;

        StartCoroutine(ToggleParentActiveCoroutine(inactiveDuration, activeDuration));
    }

    protected IEnumerator ToggleParentActiveCoroutine(float inactiveDuration, float activeDuration)
    {
        while (true) // Loop indefinitely
        {
            SetChildObjectsActive(SET_ACTIVE); // Activate the object
            yield return new WaitForSeconds(activeDuration); // Wait for the active duration

            SetChildObjectsActive(SET_DEACTIVE); // Deactivate the object
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

    public IWeaponProduct GetWeaponInfo(WeaponProduct weaponProduct)
    {
        IWeaponProduct[] weaponProducts = GetWeaponProduct(weaponProduct);
        if(weaponProducts.Length > 0)
        {
            return weaponProducts[0];
        } else 
        {
            Debug.Log("GetWeaponInfo return null");
        }

        return null;
    }

    public void LevelUp()
    {
        if(Stats.Count > 0 && StatsLevel < Stats.Count - 1)
        {
            StatsLevel++;

            statsUpdated = true;
        }
    }

    public string GetLog(IEnemyProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }

    
}

