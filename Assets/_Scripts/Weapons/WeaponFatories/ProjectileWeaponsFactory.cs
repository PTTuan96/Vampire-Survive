
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using static WeaponEnums;

public class ProjectileWeaponsFactory : WeaponFactory
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private Knife knifePrefab; 
    // Throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity;
    [SerializeField] private int maxSize = 100;

    private IObjectPool<Knife> knifePool;

    private void Awake()
    {
        CreateKnifePool(defaultCapacity, maxSize);
    }

    private void CreateKnifePool(int capacity, int maxSize)
    {
        knifePool = new ObjectPool<Knife>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionCheck, capacity, maxSize);
    }

    public void UpdateDefaultCapacity()
    {   
        int newCapacity = 0;
        if(Stats != null)
        {
           newCapacity = Stats[GetWeaponLevel(WeaponProduct.Knife)].Amount;
        }
         
        if (newCapacity < 0 || newCapacity > maxSize)
        {
            Debug.LogError("Invalid capacity");
            return;
        }

        // Destroy the current pool
        knifePool.Clear(); // Ensure to clear the current pool if ObjectPool supports a clear method

        // Create a new pool with the updated capacity
        CreateKnifePool(newCapacity, maxSize);
    }

    private Knife CreatePooledItem()
    {
        Knife knifeInstance = Instantiate(knifePrefab, parentTransform);
        knifeInstance.ObjectPool = knifePool;
        knifeInstance.UpdateStats(0, Stats[GetWeaponLevel(WeaponProduct.Knife)]);
        return knifeInstance;
    }

    private void OnTakeFromPool(Knife knife)
    {
        knife.gameObject.SetActive(true);
        knife.UpdateStats(0, Stats[GetWeaponLevel(WeaponProduct.Knife)]);
    }

    private void OnReturnedToPool(Knife knife)
    {
        knife.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Knife knife)
    {
        Destroy(knife.gameObject);
    }

    public Knife GetKnife()
    {
        return knifePool.Get();
    }

    public void ReleaseKnife(Knife knife)
    {
        knifePool.Release(knife);
    }

    public override void SetStatsWeaponEachFactory(WeaponProduct weaponProduct)
    {
        if(Stats.Count > 0)
        {
            int lvl = GetWeaponLevel(weaponProduct);
            if(lvl < 0)
            {
                return;
            }
            lvl++;
            GetAndUpdateWeaponLevel(weaponProduct, lvl);
            UpdateDefaultCapacity();
        }
    }
}
