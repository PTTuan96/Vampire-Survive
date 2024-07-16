using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;
    public float hitWaitTime = 1f;
    private float hitCounter;

    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        if (theRB != null)
        {
            theRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    void Update()
    {
        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

        if(hitCounter > 0f)
        {   
            hitCounter -= Time.deltaTime;
        }
    }


    // OnCollision 3 event
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Debug.Log("Collision started with " + collision2D.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            // Player.instance.ApplyDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("Collision ended with " + collision.gameObject.name);
    }
}
