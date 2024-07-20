using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A common interface between products
/// </summary>
public interface IEnemyProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    // public void Initialize(Transform playerTranform);

    void MoveTowardsPlayer();

    void Defend();
}
