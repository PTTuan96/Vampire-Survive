using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class GenericObjectPool<T> where T : MonoBehaviour, IPooledWeapon
{
    private readonly IObjectPool<T> objectPool;
    private readonly Transform poolParentTransform;

    public GenericObjectPool(T prefab, Transform parent, int defaultCapacity = 20, int maxSize = 100)
    {
        poolParentTransform = parent;
        objectPool = new ObjectPool<T>(() => CreateInstance(prefab), OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, defaultCapacity, maxSize);
    }

    private T CreateInstance(T prefab)
    {
        T instance = Object.Instantiate(prefab);
        instance.Pool = objectPool;
        instance.transform.SetParent(poolParentTransform);
        return instance;
    }

    private void OnReleaseToPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetFromPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(T pooledObject)
    {
        Object.Destroy(pooledObject.gameObject);
    }

    public T Get()
    {
        return objectPool.Get();
    }

    public void Release(T item)
    {
        objectPool.Release(item);
    }
}