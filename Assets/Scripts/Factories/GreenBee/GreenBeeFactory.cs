using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBeeFactory : Factory
{
    [SerializeField]
    private GreenBeeProduct m_GreenBeePrefabs;

    public override IProduct GetProduct(Vector3 position)
    {
        // Create a Prefab instance and get the product component
        GameObject instance = Instantiate(m_GreenBeePrefabs.gameObject, position, Quaternion.identity);
        GreenBeeProduct newBee = instance.GetComponent<GreenBeeProduct>();

        // Each product contains its own logic
        newBee.Initialize();

        return newBee;
    }
}
