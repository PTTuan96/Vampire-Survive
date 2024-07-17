using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponFactory[] m_WeaponFactories;

    private List<GameObject> m_CreatedProduct = new();

    void Start()
    {
        SpawnObjectWithFactory();
    }

    void SpawnObjectWithFactory()
    {
        WeaponFactory selectedFactory = m_WeaponFactories[Random.Range(0, m_WeaponFactories.Length)];
        if(selectedFactory != null)
        {
            IWeaponProduct product = selectedFactory.GetProduct(transform.position);
            if(product is Component component)
            {
                m_CreatedProduct.Add(component.gameObject);
            }
        }
    }
}
