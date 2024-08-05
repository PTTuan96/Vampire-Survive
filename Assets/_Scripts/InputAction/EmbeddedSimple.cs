using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedSimple : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private float direction;

    public InputAction moveAction;

    private bool exampleBool;

    void Start()
    {
        // moveAction.performed += Move;
        // moveAction.canceled += StopMove;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // moveAction.performed -= Move;
        // moveAction.canceled -= StopMove;
        moveAction.Disable();
    }

    private void Move(InputAction.CallbackContext value)
    {
        // Debug.Log(value.ReadValue<Vector2>().normalized);
        direction = value.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<float>();
    }

    void Update()
    {
        direction = moveAction.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, direction * speed);
    }
}
