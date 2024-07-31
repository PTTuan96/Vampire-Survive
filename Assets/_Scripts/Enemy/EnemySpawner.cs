using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private UnityEvent m_ClickToCreateEnemy;

    [SerializeField] private EnemyFactory[] m_factories;
    [SerializeField] private GameObject m_ListGreenBee;
    [SerializeField] private GameObject m_ListSlime;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float waveLength = 10f;
    [SerializeField] private float timeBetweenSpawnsWave = 2f;
    [SerializeField] private float timeToSpawn = 1f;
    [SerializeField] private float spawnDistance = 5f;

    private float waveTimer;
    private float enemySpawnTimer;
    private bool isWaveActive;

    private Dictionary<System.Type, GameObject> enemyParentMap = new Dictionary<System.Type, GameObject>();

    private List<IEnemyProduct> m_CreatedProduct = new();
    private bool isPlayerDead = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        waveTimer = timeBetweenSpawnsWave;
        enemySpawnTimer = timeToSpawn;

        // Initialize the dictionary
        enemyParentMap[typeof(GreenBeeProduct)] = m_ListGreenBee;
        enemyParentMap[typeof(SlimeProduct)] = m_ListSlime;
    }

    void Update()
    {
        if(!isPlayerDead)
        {
            UpdateTimers();
        }
    }

    private void UpdateTimers()
    {
        if (isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            enemySpawnTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                isWaveActive = false;
                waveTimer = waveLength;
                // Perform end of wave actions here if any
            }

            if (enemySpawnTimer <= 0)
            {
                enemySpawnTimer = timeToSpawn;
                SpawnObjectWithFactory();
            }
        }
        else
        {
            timeBetweenSpawnsWave -= Time.deltaTime;
            if (timeBetweenSpawnsWave <= 0)
            {
                isWaveActive = true;
                timeBetweenSpawnsWave = waveLength; // Reset the wave length timer
                // Perform start of wave actions here if any
            }
        }
    }

    private void SpawnObjectWithFactory()
    {
        Vector3 randomPosition = Utils.GetRandomPositionInCameraBounds(mainCamera, spawnDistance);
        EnemyFactory selectedFactory = m_factories[Random.Range(0, m_factories.Length)];
        
        if (selectedFactory != null)
        {
            IEnemyProduct product = selectedFactory.GetProduct(randomPosition);
            if (product != null)
            {
                m_CreatedProduct.Add(product);
                if (product is Component component)
                {
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

    public void CheckPlayerDead()
    {
        isPlayerDead = true;
        foreach(IEnemyProduct enemy in m_CreatedProduct)
        {
            enemy.StopEnemy();
        }
    }
}