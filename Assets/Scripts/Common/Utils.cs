using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Help detect layer
    public static bool ContainsLayer(this LayerMask layerMask, GameObject obj)
    {
        // Check if LayerMask includes the bitwise representation of the GameObject layer
        return ((layerMask.value & (1 << obj.layer)) != 0);
    }

    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public static Vector3 GetRandomPositionInCameraBounds(Camera mainCamera, float spawnDistance)
    {
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        bool spawnHorizontally = Random.value > 0.5f;

        float randomY;
        float randomX;

        if (spawnHorizontally)
        {
            randomX = Random.value > 0.5f ? mainCamera.transform.position.x + width / 2 + spawnDistance : mainCamera.transform.position.x - width / 2 - spawnDistance;
            randomY = Random.Range(mainCamera.transform.position.y - height / 2, mainCamera.transform.position.y + height / 2);
        }
        else
        {
            randomX = Random.Range(mainCamera.transform.position.x - width / 2, mainCamera.transform.position.x + width / 2);
            randomY = Random.value > 0.5f ? mainCamera.transform.position.y + height / 2 + spawnDistance : mainCamera.transform.position.y - height / 2 - spawnDistance;
        }

        return new Vector3(randomX, randomY, 0);
    }

        // Get names of all child Transforms
    public static List<string> GetChildNames(Transform parent)
    {
        List<string> childNames = new List<string>();
        int i = 0;
        foreach (Transform child in parent)
        {
            childNames.Add(child.name);
            i++;
        }
        
        return childNames;
    }

     // Method to get a direct child GameObject by name
    public static GameObject GetChildByName(Transform parent, string name)
    {
        Transform childTransform = parent.Find(name);
        return childTransform != null ? childTransform.gameObject : null;
    }

    // Method to create a GameObject, set its name, and make it a child of the given Transform
    public static GameObject CreateAndAttachGameObject(Transform parent, string name)
    {
        // Create a new GameObject
        GameObject newGameObject = new GameObject(name);

        // Set the parent Transform
        if (parent != null)
        {
            newGameObject.transform.SetParent(parent, false); // false means don't maintain local position
        }

        // Optionally, you can set other properties of the new GameObject here

        return newGameObject;
    }
}
