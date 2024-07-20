using UnityEngine;
using UnityEngine.Pool;

public class GreenBeeFactory : EnemyFactory
{
    [SerializeField]
    private GreenBeeProduct m_GreenBeePrefabs;

    private ObjectPool<GreenBeeProduct> objectPool;
    private Transform playerTransform;

    private int maxSize = 100;

    private void Awake()
    {
        var player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            playerTransform = player.transform;
        }

        objectPool = new ObjectPool<GreenBeeProduct>(
            CreatePooledObject,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            true, 20, maxSize
        );
    }

    private GreenBeeProduct CreatePooledObject()
    {
        var newProduct = Instantiate(m_GreenBeePrefabs);
        newProduct.ObjectPool = objectPool;
        return newProduct;
    }

    private void OnGetFromPool(GreenBeeProduct product)
    {
        product.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(GreenBeeProduct product)
    {
        product.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(GreenBeeProduct product)
    {
        Destroy(product.gameObject);
    }

    public override IEnemyProduct GetProduct(Vector3 position)
    {
        GreenBeeProduct product;
        if (objectPool.CountActive >= maxSize && objectPool.CountInactive == 0)
        {
            product = Instantiate(m_GreenBeePrefabs, position, Quaternion.identity);
            product.Initialize(playerTransform, false);
        }
        else
        {
            product = objectPool.Get();
            product.transform.position = position;
            product.Initialize(playerTransform, true);
        }

        return product;
    }
}
