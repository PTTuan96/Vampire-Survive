using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : MonoBehaviour, IWeaponProduct
{
    [SerializeField] private GameObject holderPrefab;

    [Tooltip("This name have to match 100% with the holder gameobject name")]
    [SerializeField] private string productName;

    public string ProductName { get => productName; set => productName = value; }
    public GameObject HolderPrefab { get => holderPrefab; set => holderPrefab = value; }
    
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
