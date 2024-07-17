
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // This method is called once when the collision starts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision started with " + collision.gameObject.name);
        // Add your logic for when the collision starts
    }

    // This method is called every frame while the collision is happening
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision staying with " + collision.gameObject.name);
        // Add your logic for continuous collision detection
    }

    // This method is called once when the collision ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision ended with " + collision.gameObject.name);
        // Add your logic for when the collision ends
    }
}
