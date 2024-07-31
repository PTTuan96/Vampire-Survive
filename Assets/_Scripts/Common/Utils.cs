using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static List<GameObject> GetAllChildren(GameObject parent)
    {
        return parent.transform.Cast<Transform>().Select(t => t.gameObject).ToList();
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
            // newGameObject.tag = name; -> can't use because have to declare first
        }

        // Optionally, you can set other properties of the new GameObject here

        return newGameObject;
    }

    public static Vector3 GetParentTranform(Transform child)
    {
        if(child == null)
        {
            Debug.Log("child object not found");
        }
        if(child.parent == null)
        {
            Debug.Log("child object not found");
            return Vector3.zero;
        }
        return child.parent.position;
    }

    public static int GetLengthListByName(string parentName)
    {
        // Find the parent GameObject by name
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject != null)
        {
            // Get the number of children of the parent GameObject
            return parentObject.transform.childCount;
        }
        else
        {
            Debug.Log("Parent object not found");
        }

        return 0;
    }

    public static void HorizontalMove(float p_CurrentAngle, float p_OrbitDistance ,out float x, out float z)
    {
        // Calculate the new position using polar coordinates (horizontal obit)
        x = Mathf.Cos(p_CurrentAngle) * p_OrbitDistance;
        z = Mathf.Sin(p_CurrentAngle) * p_OrbitDistance;

        // How to use ->
            // Utils.HorizontalMove(p_CurrentAngle, p_OrbitDistance, out float x, out float y);
            // transform.position = new Vector3(x, 0, y) + parentObject;
    }

    public static void OrbitMove(float p_CurrentAngle, float p_OrbitDistance ,out float x, out float y)
    {
        // Calculate the new position using polar coordinates (horizontal obit)
        x = Mathf.Cos(p_CurrentAngle) * p_OrbitDistance;
        y = Mathf.Sin(p_CurrentAngle) * p_OrbitDistance;

        // How to use ->
            // Utils.HorizontalMove(p_CurrentAngle, p_OrbitDistance, out float x, out float y);
            // transform.position = new Vector3(x, 0, y) + parentObject;
    }
}
