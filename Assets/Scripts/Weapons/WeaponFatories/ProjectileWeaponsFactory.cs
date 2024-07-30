using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using static WeaponEnums;

public class ProjectileWeaponsFactory : WeaponFactory
{
    [SerializeField] private Transform poolParentTransform;

    [SerializeField] private Knife KnifeWeaponPrefabs;

    [Tooltip("Projectile force")]
    [SerializeField] private float muzzleVelocity = 1500f;

    [Tooltip("End point of gun where shots appear")]
    [SerializeField] private Transform muzzlePosition;

    [Tooltip("Time between shots / smaller = higher rate of fire")]
    [SerializeField] private float cooldownWindow = 0.1f;

    [SerializeField] private UnityEvent m_KnifeThrowed;

    // Stack-based ObjectPool available with Unity 2021 and above
    private IObjectPool<Knife> objectPool;

    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;
    private float nextTimeToShoot;

    private void Awake()
    {
        objectPool = new ObjectPool<Knife>(CreateWeaponProduct,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    private IWeaponProduct CreateWeaponProduct()
    {
        Knife projectileInstance = Instantiate(KnifeWeaponPrefabs);
        projectileInstance.ObjectPool = objectPool;
        projectileInstance.transform.SetParent(poolParentTransform); // Set the parent
        return projectileInstance;
    }

    // Invoked when returning an item to the object pool
    private void OnReleaseToPool(Knife pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // Invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Knife pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
        pooledObject.transform.SetParent(poolParentTransform); // Ensure parent is set
    }

    // Invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Knife pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Knife GetKnife()
    {
        return objectPool.Get();
    }

    public void ReleaseKnife(Knife knife)
    {
        objectPool.Release(knife);
    }

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        
    }
}
