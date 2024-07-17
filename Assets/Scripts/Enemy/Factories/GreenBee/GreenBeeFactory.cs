using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBeeFactory : EnemyFactory
{
    [SerializeField]
    private GreenBeeProduct m_GreenBeePrefabs;

    private Transform playerTransform;

    private void Awake()
    {
        // use this to find player or just can use Tag
        var player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public override IEnemyProduct GetProduct(Vector3 position)
    {
        // Create a Prefab instance and get the product component
        GameObject instance = Instantiate(m_GreenBeePrefabs.gameObject, position, Quaternion.identity);
        GreenBeeProduct newBee = instance.GetComponent<GreenBeeProduct>();

        // Assign the player transform to the newBee
        if (playerTransform != null)
        {
            newBee.Initialize(playerTransform);
        }
        else
        {
            newBee.Initialize(); // Call initialize without playerTransform if player is null
        }

        return newBee;
    }
}
