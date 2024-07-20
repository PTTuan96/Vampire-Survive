using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _objects = new Queue<T>();
    private readonly T _prefab;
    private readonly Transform _parentTransform;
    private readonly int _maxSize;
    private int _countAll;

    public int CountAll => _countAll;
    public int CountInactive => _objects.Count;

    public CustomObjectPool(T prefab, int maxSize, Transform parentTransform = null)
    {
        _prefab = prefab;
        _maxSize = maxSize;
        _parentTransform = parentTransform;
    }

    private T CreateObject()
    {
        T newObj = Object.Instantiate(_prefab, _parentTransform);
        newObj.gameObject.SetActive(false);
        _countAll++;
        return newObj;
    }

    public void AddToPool()
    {
        if (_countAll < _maxSize)
        {
            T newObj = CreateObject();
            _objects.Enqueue(newObj);
        }
        else
        {
            Debug.LogWarning("Pool has reached its maximum size. Cannot add more objects.");
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        if (_objects.Count > 0)
        {
            T obj = _objects.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else if (_countAll < _maxSize)
        {
            T newObj = CreateObject();
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
            newObj.gameObject.SetActive(true);
            return newObj;
        }
        else
        {
            Debug.LogWarning("Pool has reached its maximum size.");
            return null;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }
}