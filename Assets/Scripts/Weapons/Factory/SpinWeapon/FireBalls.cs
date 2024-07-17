using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : MonoBehaviour
{   
    public float rotationSpeed = 360f; // Degrees per second

    void Update()
    {
        // Rotate around Z-axis
        transform.Rotate(0, 0, rotationSpeed * 0.5f * Time.deltaTime);
    }
}
