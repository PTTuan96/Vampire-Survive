using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwepDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;
    [SerializeField]
    private GameObject trail;

    private PlayerController playerController;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine coroutine;

    private void Awake()
    {
        playerController = PlayerController.Instance;
    }

    private void OnEnable()
    {
        playerController.OnStartTouch += SwipeStart;
        playerController.OnEndTouch += SwipeEnd; 
    }

    private void OnDisable()
    {
        playerController.OnStartTouch -= SwipeStart;
        playerController.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while(true)
        {
            trail.transform.position = playerController.PrimaryPosition();
            // yield return new WaitForSeconds(3f);
            yield return null; // -> return the next frame to continue
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance 
            && (endTime - startTime) <= maximumTime)
        {
            // Debug.Log("Swipe Detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

            // normalized is way convert from Vector3 to Vector2
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

            SwipeDetection(direction2D);
        }
    }

    private void SwipeDetection(Vector2 direction)
    {
        // dot product -> caculate angle base Vector a and b (Vector2)
        // Pointing Same derection -> return 1 or else oposite derection return -1
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold )
        {
            // Debug.Log("Swipe Up");
        } 
        else if(Vector2.Dot(Vector2.down, direction) > directionThreshold )
        {
            // Debug.Log("Swipe Down");
        }
        else if(Vector2.Dot(Vector2.left, direction) > directionThreshold )
        {
            // Debug.Log("Swipe Left");
        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold )
        {
            // Debug.Log("Swipe Right");
        }
    }
}
