using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeaponFactory : WeaponFactory
{
    [SerializeField]
    private FireBallProduct m_SpinWeaponPrefabs;
    [SerializeField]
    private GameObject m_FireBalls;

    public override IWeaponProduct GetProduct(Vector3 position)
    {
        float x = Mathf.Cos(0);
        float y = Mathf.Sin(0);
        // Create a Prefab instance and get the product component
        GameObject fireBallInstance = Instantiate(m_SpinWeaponPrefabs.gameObject, new Vector3(x, y, 0), Quaternion.identity);
        FireBallProduct fireBall = fireBallInstance.GetComponent<FireBallProduct>();

        fireBall.Initialize(); // Call initialize without playerTransform if player is null
        
        // Add fireBall instance to m_FireBalls
        if (m_FireBalls != null)
        {
            fireBallInstance.transform.SetParent(m_FireBalls.transform);
        }
        else
        {
            Debug.LogWarning("m_FireBalls GameObject is not assigned.");
        }

        return fireBall;
    }
}
