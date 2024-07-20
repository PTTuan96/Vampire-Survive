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
    [SerializeField] private GameObject m_ListSlime;
    [SerializeField] private Camera mainCamera;

    private float spawnCounter;
    public float spawnDistance = 5f;

    private Dictionary<System.Type, GameObject> enemyParentMap = new Dictionary<System.Type, GameObject>();

    private List<GameObject> m_CreatedProduct = new();

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        spawnCounter = timeToSpawn;

        // Initialize the dictionary
        enemyParentMap[typeof(GreenBeeProduct)] = m_ListGreenBee;
        enemyParentMap[typeof(SlimeProduct)] = m_ListSlime;
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
            if (product != null)
            {
                if (product is Component component)
                {
                    m_CreatedProduct.Add(component.gameObject);

                    GameObject parent;
                    if (enemyParentMap.TryGetValue(product.GetType(), out parent))
                    {
                        if (parent != null)
                        {
                            component.gameObject.transform.SetParent(parent.transform);
                        }
                        else
                        {
                            Debug.LogWarning($"Parent GameObject for {product.GetType().Name} is not assigned.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No parent GameObject mapped for {product.GetType().Name}.");
                    }
                }
            }
        }
    }
}