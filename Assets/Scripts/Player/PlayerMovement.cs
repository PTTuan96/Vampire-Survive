using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float m_Speed;

    public void Move(Vector3 moveInput)
    {
        if (moveInput != Vector3.zero)
        {
            transform.position += m_Speed * Time.deltaTime * moveInput.normalized;
        }
    }
}







// Old code





// public class PlayerMovement : MonoBehaviour
// {

//     [SerializeField]
//     private float moveSpeed;

//     private PlayerActions playerController;
//     public Animator animator;

//     Vector3 moveInput = new Vector3();
//     private float positionX;
//     private float positionY;


//     //
//     private Coroutine holdCoroutine;

//     private float holdStartTime;
//     Vector2 positionCheck; 
//     //

//     private void Awake()
//     {
//         playerController = PlayerActions.Instance;
//     }

//     private void OnEnable()
//     {
//         // Vector2 Movement
//         playerController.OnMovementStart += MoveStart; // -> this is a supscriber
//         playerController.OnMovementStartWithPressPoint += MoveStartWithPressPoint; 

//         playerController.OnMovementPerform += Moving;
//         playerController.OnMovementPerformWithPressPoint += MovePerformWithPressPoint; 
        
//         playerController.OnMovementEnd += MoveEnd;

//         // Axis
//         playerController.OnStartMoveAxis += StartMoveAxis;
//         playerController.OnMovingAxis += MovingAxis;
//         playerController.OnEndMoveAxis += EndMoveAxis;
//     }

//     private void OnDisable()
//     {
//         // Vector2
//         playerController.OnMovementStart -= MoveStart;
//         playerController.OnMovementStartWithPressPoint -= MoveStartWithPressPoint;

//         playerController.OnMovementPerform -= Moving;
//         playerController.OnMovementPerformWithPressPoint -= MovePerformWithPressPoint;
        
//         playerController.OnMovementEnd -= MoveEnd; 

//         // Axis
//         playerController.OnStartMoveAxis -= StartMoveAxis;
//         playerController.OnStartMoveAxis -= MovingAxis;
//         playerController.OnStartMoveAxis -= EndMoveAxis;
//     }

//     private void Update()
//     {
//         moveInput.x = positionX;
//         moveInput.y = positionY;
        
//         // It help to player can move smother not only 0, 45, 90 Degre anymore
//         moveInput.Normalize();

//         // DeltaTime is for after one frame , if not it gonna move realy fast
//         transform.position += moveInput * moveSpeed * Time.deltaTime;  

//         if(moveInput != Vector3.zero)
//         {
//             animator.SetBool("IsMoving", true);
//         } else
//         {
//             animator.SetBool("IsMoving", false);
//         }
//     }

//     // Axis Move
//     private void StartMoveAxis(float value, float time, string moveType)
//     {
//         // Debug.Log("-----MoveStart----");
//         // Debug.Log(" started time : " + time);
//     }

//     private void MovingAxis(float value, float time, string moveType)
//     {
//         // Debug.Log("-----Moving Type ---- : " + moveType);
//         // Debug.Log("-----Moving Value ---- : " + value);
//         MoveAxis(value, moveType);
//     }

//     private void EndMoveAxis(float value, float time, string moveType)
//     {
//         MoveAxis(value, moveType, true);
//         // Debug.Log("-----MoveStart----");
//         // Debug.Log(" started time : " + time);
//     }

//     private void MoveAxis(float value, string moveType, bool isEndMove = false)
//     {
//         if (moveType.Equals("Horizontal"))
//         {
//             positionX = isEndMove ? 0 : value;
//         }
//         else if (moveType.Equals("Vertical"))
//         {
//             positionY = isEndMove ? 0 : value;
//         }
//     }

//     // Vector2 Move
//     private void MoveStart(Vector2 worldPosition, float time)
//     {
//         // Debug.Log("-----MoveStart----");
//         // Debug.Log(" started time : " + time);
//     }

//     private void MoveStartWithPressPoint(Vector2 position, float time)
//     {
//         // Debug.Log("-----MoveStart----"); 
//         // Debug.Log("Hold action started");
//         // Debug.Log("Position: "  + position);
//     }

//     private void Moving(Vector2 worldPosition, float time)
//     {
//         // if check interactions ->  have to be larger than the value to be consider a hold 
//         //  Hold Press Point -> is the value of the Vector2 -> like the distance > 0.5f, 1f

//         // Debug.Log("-----Is Moving---- AFter Time: "  + time);
//         // Debug.Log("Moving started");
//     }

//     private void MovePerformWithPressPoint(Vector2 position, float time)
//     {
//         // if check interactions ->  have to be larger than the value to be consider a hold 
//         //  Hold Press Point -> is the value of the Vector2 -> like the distance > 0.5f, 1f

//         // Debug.Log("-----Is Moving---- AFter Delta Position: "  + position);
//     }

//     private void MoveEnd(Vector2 position, float time)
//     {
//         // Debug.Log("-----MoveEnd----");
//     }
// }