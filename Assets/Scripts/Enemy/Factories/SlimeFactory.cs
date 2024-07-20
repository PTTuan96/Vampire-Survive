using UnityEngine;
using UnityEngine.Pool;

public class SlimeFactory //: EnemyFactory
{
    // [SerializeField]
    // private SlimeProduct m_SlimePrefabs;

    // private ObjectPool<SlimeProduct> objectPool;
    // private Transform playerTransform;

    // private int maxSize = 100;

    // private void Awake()
    // {
    //     var player = FindObjectOfType<PlayerMovement>();
    //     if (player != null)
    //     {
    //         playerTransform = player.transform;
    //     }

    //     objectPool = new ObjectPool<SlimeProduct>(
    //         CreatePooledObject,
    //         OnGetFromPool,
    //         OnReleaseToPool,
    //         OnDestroyPooledObject,
    //         true, 20, maxSize
    //     );
    // }

    // private SlimeProduct CreatePooledObject()
    // {
    //     var newProduct = Instantiate(m_SlimePrefabs);
    //     newProduct.ObjectPool = objectPool;
    //     return newProduct;
    // }

    // private void OnGetFromPool(SlimeProduct product)
    // {
    //     product.gameObject.SetActive(true);
    // }

    // private void OnReleaseToPool(SlimeProduct product)
    // {
    //     product.gameObject.SetActive(false);
    // }

    // private void OnDestroyPooledObject(SlimeProduct product)
    // {
    //     Destroy(product.gameObject);
    // }

    // public override IEnemyProduct GetProduct(Vector3 position)
    // {
    //     SlimeProduct product;
    //     if (objectPool.CountActive <= maxSize && objectPool.CountInactive == 0)
    //     {
    //         product = Instantiate(m_SlimePrefabs, position, Quaternion.identity);
    //         product.Initialize(playerTransform, false);
    //     }
    //     else
    //     {
    //         product = objectPool.Get();
    //         product.transform.position = position;
    //         product.Initialize(playerTransform, true);
    //     }

    //     return product;
    // }
}