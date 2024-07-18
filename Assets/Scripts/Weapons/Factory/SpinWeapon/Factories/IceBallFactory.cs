using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallFactory : SpinWeaponFactory
{
    [SerializeField]
    private IceBallProduct m_IceBallPrefab;

    [SerializeField]
    private int m_NumIceBalls = 3; // Number of IceBalls to spawn

    [SerializeField]
    private float iceBallRadiusIncrement = 1f;
    void Start()
    {
        if (m_ProductParent != null)
        {
            SpawnMultipleProducts(transform.position);
        }
    }

    public override void SpawnMultipleProducts(Vector3 position)
    {
        for (int i = 0; i < m_NumIceBalls; i++)
        {
            float angle = i * Mathf.PI * 2 / m_NumIceBalls;
            float x = Mathf.Cos(angle) * iceBallRadiusIncrement;
            float y = Mathf.Sin(angle) * iceBallRadiusIncrement;

            Vector3 spawnPosition = position + new Vector3(x, y, 0);
            GameObject iceBallInstance = Instantiate(m_IceBallPrefab.gameObject, spawnPosition, Quaternion.identity);
            InitializeProduct(iceBallInstance);
        }
    }

    public override IWeaponProduct GetProduct(Vector3 position)
    {
        float x = Mathf.Cos(0);
        float y = Mathf.Sin(0);
        GameObject iceBallInstance = Instantiate(m_IceBallPrefab.gameObject, new Vector3(x, y, 0), Quaternion.identity);
        InitializeProduct(iceBallInstance);
        return iceBallInstance.GetComponent<IWeaponProduct>();
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
