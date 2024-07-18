using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Time between spawns / smaller = higher rate of fire")]
    [SerializeField] private float timeToSpawn;
    [SerializeField] private UnityEvent m_ClickToCreateEnemy;

    [SerializeField] private EnemyFactory[] m_factories;
    [SerializeField] private GameObject m_ListGreenBee;

    private List<GameObject> m_CreatedProduct = new();

    [SerializeField] private Camera mainCamera;

    private float spawnCounter;
    public float spawnDistance = 5f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        spawnCounter = timeToSpawn;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;
            SpawnObjectWithFactory();
        }
    }

    void SpawnObjectWithFactory()
    {
        Vector3 randomPosition = Utils.GetRandomPositionInCameraBounds(mainCamera, spawnDistance);
        EnemyFactory selectedFactory = m_factories[Random.Range(0, m_factories.Length)];
        if (selectedFactory != null)
        {
            IEnemyProduct product = selectedFactory.GetProduct(randomPosition);
            if (product is Component component)
            {
                m_CreatedProduct.Add(component.gameObject);

                if (m_ListGreenBee != null)
                {
                    component.gameObject.transform.SetParent(m_ListGreenBee.transform);
                }
                else
                {
                    Debug.LogWarning("m_ListGreenBee GameObject is not assigned.");
                }
            }
        }
    }
}