using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponFactory : MonoBehaviour
{
    [SerializeField] protected List<Attributes> stats;
    [SerializeField] protected int weaponLevel;

    [Tooltip("Notifies listeners of updated Stats percentage")]
    public UnityEvent<float> StatsChange; 

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

