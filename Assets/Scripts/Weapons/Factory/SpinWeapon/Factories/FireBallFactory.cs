using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallFactory : SpinWeaponFactory
{
    [SerializeField]
    private FireBallProduct m_FireBallPrefab;

    [SerializeField]
    private int m_NumFireBalls = 5; // Number of FireBalls to spawn

    [SerializeField]
    private float fireBallRadiusIncrement = 2f; // Adjust this value as needed

    void Start()
    {
        if (m_ProductParent != null)
        {
            SpawnMultipleProducts(transform.position);
        }
    }

    public override void SpawnMultipleProducts(Vector3 position)
    {
        for (int i = 0; i < m_NumFireBalls; i++)
        {
            float angle = i * Mathf.PI * 2 / m_NumFireBalls;
            float x = Mathf.Cos(angle) * fireBallRadiusIncrement;
            float y = Mathf.Sin(angle) * fireBallRadiusIncrement;

            Vector3 spawnPosition = position + new Vector3(x, y, 0);
            GameObject fireBallInstance = Instantiate(m_FireBallPrefab.gameObject, spawnPosition, Quaternion.identity);
            InitializeProduct(fireBallInstance);
        }
    }

    public override IWeaponProduct GetProduct(Vector3 position)
    {
        float x = Mathf.Cos(0);
        float y = Mathf.Sin(0);
        GameObject fireBallInstance = Instantiate(m_FireBallPrefab.gameObject, new Vector3(x + 1, y + 1, 0), Quaternion.identity);
        InitializeProduct(fireBallInstance);
        return fireBallInstance.GetComponent<IWeaponProduct>();
    }

    void Update()
    {
        if (m_ProductParent != null)
        {
            var products = m_ProductParent.GetComponentsInChildren<IWeaponProduct>();
            if (products.Length > 0)
            {
                float averageSpeed = CalculateAverageSpeed(products);
                UpdateParentRotation(averageSpeed);
                UpdateChildProducts(products);
            }
        }
    }

    protected void UpdateChildProducts(IWeaponProduct[] products)
    {
        foreach (var product in products)
        {
            // Implement child-specific updates if needed
        }
    }

}
