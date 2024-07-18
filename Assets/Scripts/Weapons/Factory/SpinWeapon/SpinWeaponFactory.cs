using UnityEngine;

public abstract class SpinWeaponFactory : WeaponFactory
{
    [SerializeField]
    protected GameObject m_ProductParent;

    public abstract void SpawnMultipleProducts(Vector3 position);

    protected void InitializeProduct(GameObject productInstance)
    {
        IWeaponProduct product = productInstance.GetComponent<IWeaponProduct>();
        product.Initialize();

        if (m_ProductParent != null)
        {
            productInstance.transform.SetParent(m_ProductParent.transform);
        }
        else
        {
            Debug.LogWarning("m_ProductParent GameObject is not assigned.");
        }
    }

    protected void UpdateParentRotation(float rotationSpeed)
    {
        if (m_ProductParent != null)
        {
            m_ProductParent.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // The more Object the speed had to reduce
    protected float CalculateAverageSpeed(IWeaponProduct[] products)
    {
        float totalSpeed = 0f;
        foreach (var product in products)
        {
            totalSpeed += product.RotationSpeed; // Adjust as needed based on your specific speed setup
        }
        return totalSpeed / products.Length;
    }

    protected void IncreaseRadius(IWeaponProduct product, float radiusIncrement)
    {
        Vector3 position = (product as MonoBehaviour).transform.position;
        position.x += Mathf.Cos(Time.time) * radiusIncrement;
        position.y += Mathf.Sin(Time.time) * radiusIncrement;
        (product as MonoBehaviour).transform.position = position;
    }
}
