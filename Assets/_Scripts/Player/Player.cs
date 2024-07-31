using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput), typeof(PlayerAudio), typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("LayerMask to identify obstacles in the game environment.")]
    LayerMask m_ObstacleLayer;

    PlayerInput m_PlayerInput;
    PlayerMovement m_PlayerMovement;

    // Player Sound, video
    PlayerAudio m_PlayerAudio;
    
    // Player Effects
    PlayerFX m_PlayerFX;

    private void Awake()
    {
        Initialize();
    }

    // Sets up component references.
    private void Initialize()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerAudio = GetComponent<PlayerAudio>();
        m_PlayerFX = GetComponent<PlayerFX>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(m_ObstacleLayer.ContainsLayer(hit.gameObject))
        {
            // Play a random audio clip on collision.
            // m_PlayerAudio.PlayRandomClip();

            // Trigger visual effect, if defined.
            // if (m_PlayerFX != null)
            //     m_PlayerFX.PlayEffect();
        }
    }

    private void LateUpdate()
    {
        // Get the input vector from the PlayerInput component.
        Vector3 inputVector = m_PlayerInput.InputVector;

        // Move the player based on the input vector.
        m_PlayerMovement.Move(inputVector);
    }
}
