using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBalls : MonoBehaviour, IWeaponProduct
{
    // [Tooltip("Why this cannot attached by any Gameobject?")]
    // [SerializeField] private GameObject holderPrefab;

    [Tooltip("This name have to match 100% with the holder gameobject name")]
    [SerializeField] private string holderName;

    public string HolderWeaponName { get => holderName; set => holderName = value; }
    // public GameObject HolderPrefab { get => holderPrefab; set => holderPrefab = value; }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
