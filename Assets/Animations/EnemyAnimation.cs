using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Like using Particle System
public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite;

    public float speed;

    public float minSize, maxSize;

    private float activeSize;

    void Start()
    {
        activeSize = maxSize;

        speed *= Random.Range(.75f, 1.25f);
    }

    void Update()
    {
        sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);
    
        if(sprite.localScale.x == activeSize)
        {
            if(activeSize == maxSize)
            {
                activeSize = minSize;
            } else
            {
                activeSize = maxSize;
            }
        }
    }
}
