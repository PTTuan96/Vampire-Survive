using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] private GameObject enemyToSpawn;

    [SerializeField]
    private EnemyFactory[] m_factories;

    private List<GameObject> m_CreatedProduct = new();

    [SerializeField]
    private Camera mainCamera;
    public float timeToSpawn;
    private float spawnCounter;
    // public float spawnOffset = 1f;
    public float spawnDistance = 5f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        spawnCounter = timeToSpawn;
        // StartCoroutine(CheckAndRemoveObjects());
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            SpawnObjectWithFactory();
            // SpawnObjectOutsideCamera();
        }
    }

    void SpawnObjectWithFactory()
    {
        Vector3 randomPosition = GetRandomPositionInCameraBounds();
        EnemyFactory selectedFactory = m_factories[Random.Range(0, m_factories.Length)];
        if(selectedFactory != null)
        {
            IEnemyProduct product = selectedFactory.GetProduct(randomPosition);
            if(product is Component component)
            {
                m_CreatedProduct.Add(component.gameObject);
            }
        }
    }

    Vector3 GetRandomPositionInCameraBounds()
    {
        // Get the camera boundaries in world space
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;


        // Randomly decide whether to spawn on the left/right or top/bottom
        bool spawnHorizontally = Random.value > 0.5f;

        float randomY;
        float randomX;

        if (spawnHorizontally)
        {
            // Spawn to the left or right of the camera view
            randomX = Random.value > 0.5f ? mainCamera.transform.position.x + width / 2 + spawnDistance : mainCamera.transform.position.x - width / 2 - spawnDistance;
            randomY = Random.Range(mainCamera.transform.position.y - height / 2, mainCamera.transform.position.y + height / 2);
        }
        else
        {
            // Spawn above or below the camera view
            randomX = Random.Range(mainCamera.transform.position.x - width / 2, mainCamera.transform.position.x + width / 2);
            randomY = Random.value > 0.5f ? mainCamera.transform.position.y + height / 2 + spawnDistance : mainCamera.transform.position.y - height / 2 - spawnDistance;
        }

        return new Vector3(randomX, randomY, 0);
    }

    // private List<GameObject> spawnedObjects = new List<GameObject>();
    // private Dictionary<GameObject, float> objectTimeTracker = new Dictionary<GameObject, float>();
    // public float maxDistance = 10f; // Maximum distance before removing the object
    // public float checkInterval = 1f; // Interval for checking distance
    // public float removalDelay = 10f; // Delay before removing the object if far away

    // IEnumerator CheckAndRemoveObjects()
    // {
        // while (true)
        // {
        //     List<GameObject> objectsToRemove = new List<GameObject>();

        //     foreach (GameObject obj in spawnedObjects)
        //     {
        //         if (obj == null) continue;

        //         float distance = Vector3.Distance(mainCamera.transform.position, obj.transform.position);

        //         if (distance > maxDistance)
        //         {
        //             if (!objectTimeTracker.ContainsKey(obj))
        //             {
        //                 objectTimeTracker[obj] = Time.time;
        //             }

        //             if (Time.time - objectTimeTracker[obj] > removalDelay)
        //             {
        //                 objectsToRemove.Add(obj);
        //             }
        //         }
        //         else
        //         {
        //             if (objectTimeTracker.ContainsKey(obj))
        //             {
        //                 objectTimeTracker.Remove(obj);
        //             }
        //         }
        //     }

        //     foreach (GameObject obj in objectsToRemove)
        //     {
        //         spawnedObjects.Remove(obj);
        //         Destroy(obj);
        //     }

        //     yield return new WaitForSeconds(checkInterval);
        // }
    // }
}
