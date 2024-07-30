using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static WeaponEnums;

public abstract class WeaponFactory : MonoBehaviour
{
    // [Tooltip("List of throw weapon prefabs")]
    // [SerializeField] private List<GameObject> weaponProductPrefabs;

    protected UIController uIController;

    [SerializeField] public WeaponType WeaponType; 
    [SerializeField] public List<Attributes> Stats;
    [SerializeField] public int StatsLevel; // use for initiate List Stats

    // Create a dictionary to store WeaponType and its corresponding level
    protected Dictionary<WeaponProduct, int> weaponLevels = new();

    private void Awake()
    {
        uIController = UIController.Instance;
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
            // Debug.Log("Current Level of " + weaponProduct + ": " + currentLevel);

            // Update the level
            weaponLevels[weaponProduct] = newLevel;

            // Print or use the updated level if needed
            // Debug.Log("Updated Level of " + weaponProduct + ": " + newLevel);
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

    public void InitiateStatsLevel()
    {
        if(Stats.Count > 0 && StatsLevel < Stats.Count - 1)
        {
            
        }
    }
}

