using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions playerActions;

    // Private members
    private Vector3 m_InputVector;
    private float positionX;
    private float positionY;

    public Vector3 InputVector => m_InputVector;

    private void Awake()
    {
        playerActions = PlayerActions.Instance;
    }

    private void OnEnable()
    {
        // Axis
        playerActions.OnStartMoveAxis += StartMoveAxis;
        playerActions.OnMovingAxis += MovingAxis;
        playerActions.OnEndMoveAxis += EndMoveAxis;
    }

    private void OnDisable()
    {
        // Axis
        playerActions.OnStartMoveAxis -= StartMoveAxis;
        playerActions.OnStartMoveAxis -= MovingAxis;
        playerActions.OnStartMoveAxis -= EndMoveAxis;
    }

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        m_InputVector = new Vector2(positionX, positionY);
    }

    private void StartMoveAxis(float value, float time, string moveType)
    {
        //
    }

    private void MovingAxis(float value, float time, string moveType)
    {
        MoveAxis(value, moveType);
    }

    private void EndMoveAxis(float value, float time, string moveType)
    {
        MoveAxis(value, moveType, true);
    }
    
    private void MoveAxis(float value, string moveType, bool isEndMove = false)
    {
        if (moveType.Equals("Horizontal"))
        {
            positionX = isEndMove ? 0 : value;
        }
        else if (moveType.Equals("Vertical"))
        {
            positionY = isEndMove ? 0 : value;
        }
    }
}



// public class PlayerInput : MonoBehaviour
// {
//     // Inspector fields
//     [Header("Controls")]
//     [Tooltip("Use WASD keys to move")]
//     [SerializeField] private KeyCode m_ForwardKey = KeyCode.W;
//     [SerializeField] private KeyCode m_BackwardKey = KeyCode.S;
//     [SerializeField] private KeyCode m_LeftKey = KeyCode.A;
//     [SerializeField] private KeyCode m_RightKey = KeyCode.D;

//     // Private members
//     private Vector3 m_InputVector;
//     private float m_XInput;
//     private float m_ZInput;
//     private float m_YInput;

//     // Properties
//     public Vector3 InputVector => m_InputVector;

//     // MonoBehaviour methods
//     private void Update()
//     {
//         HandleInput();
//     }

//     // Methods
//     public void HandleInput()
//     {

//         // Reset input values to zero at the beginning of each frame
//         m_XInput = 0;
//         m_ZInput = 0;

//         if (Input.GetKey(m_ForwardKey))
//         {
//             m_ZInput++;
//         }

//         if (Input.GetKey(m_BackwardKey))
//         {
//             m_ZInput--;
//         }

//         if (Input.GetKey(m_LeftKey))
//         {
//             m_XInput--;
//         }

//         if (Input.GetKey(m_RightKey))
//         {
//             m_XInput++;
//         }

//         m_InputVector = new Vector3(m_XInput, m_YInput, m_ZInput);
//     }
// }