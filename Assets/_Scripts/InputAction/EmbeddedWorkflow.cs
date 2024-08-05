using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedWorkflow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction;

    public InputAction jumpAction;
    public InputAction moveAction;

    private bool exampleBool;

    void Start()
    {
        jumpAction.performed += Jump;
        jumpAction.canceled += StopJump;
        jumpAction.Enable();

        moveAction.performed += Move;
        moveAction.canceled += StopMove;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        jumpAction.canceled -= StopJump;
        jumpAction.Disable();

        moveAction.performed -= Move;
        moveAction.canceled -= StopMove;
        moveAction.Disable();
    }

    private void Move(InputAction.CallbackContext value)
    {
        // Debug.Log(value.ReadValue<Vector2>().normalized);
        direction = value.ReadValue<Vector2>().normalized;
    }

    private void StopMove(InputAction.CallbackContext value)
    {
        // Debug.Log(value.ReadValue<Vector2>().normalized);
        direction = value.ReadValue<Vector2>().normalized;
    }

    private void Jump(InputAction.CallbackContext value)
    {
        // exampleBool = true;
        exampleBool = value.ReadValueAsButton();
        Debug.Log("Jump performed and bool is: " + exampleBool);
    }

    private void StopJump(InputAction.CallbackContext value)
    {
        // exampleBool = false;
        exampleBool = value.ReadValueAsButton();
    }

    void Update()
    {
        // Debug.Log(moveAction.ReadValue<Vector2>().normalized);
        // direction = value.ReadValue<Vector2>().normalized;

        if(exampleBool)
        {

        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if(exampleBool)
        {

        }
    }
}
