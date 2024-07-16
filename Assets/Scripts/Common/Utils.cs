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
}
