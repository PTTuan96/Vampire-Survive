using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviourSingleton<PlayerActions>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;


    // Vector2 Start

    // Create an event
    public delegate void StartEvent(Vector2 position, float time);
    
    // This is a producer send message to another supscriber to recive message at Movement Detection
    public event StartEvent OnMovementStart; 

    public delegate void StartWithPressPointEvent(Vector2 position, float time);
    public event StartWithPressPointEvent OnMovementStartWithPressPoint;

    // Vector2 Perform
    public delegate void PerformEvent(Vector2 position, float time);
    public event PerformEvent OnMovementPerform;

    public delegate void PerformWithPressPointEvent(Vector2 position, float time);
    public event PerformWithPressPointEvent OnMovementPerformWithPressPoint;

    // Vector2 End
    public delegate void MovementEnd(Vector2 position, float time);
    public event MovementEnd OnMovementEnd;
    

    
    // Axis
    public delegate void StartMoveAxisEvent(float position, float time, string moveType);
    public event StartMoveAxisEvent OnStartMoveAxis;

    public delegate void MovingAxisEvent(float position, float time, string moveType);
    public event MovingAxisEvent OnMovingAxis;

    public delegate void EndMoveAxisEvent(float position, float time, string moveType);
    public event EndMoveAxisEvent OnEndMoveAxis;
    #endregion

    private ActionsController actionsController;

    private Camera mainCamera;

    private void Awake()
    {
        actionsController = new ActionsController();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        actionsController.Enable();
    }

    private void OnDisable()
    {
        actionsController.Disable();
    }

    void Start()
    {
        // actionsController.Player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        // actionsController.Player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        // No need to -= ctx here at onDisable beacuse it can use for another class to call
        // actionsController.Player.Movement.started += ctx => StartMove(ctx);
        // actionsController.Player.Movement.performed += ctx => OnMove(ctx);
        // actionsController.Player.Movement.canceled += ctx => EndMove(ctx); 

        // Axis movement
        actionsController.PlayerAxis.HorizontalMovement.started += ctx => StartMoveAxis(ctx, "Horizontal");
        actionsController.PlayerAxis.HorizontalMovement.performed += ctx => MovingAxis(ctx, "Horizontal");
        actionsController.PlayerAxis.HorizontalMovement.canceled += ctx => EndMoveAxis(ctx, "Horizontal");

        actionsController.PlayerAxis.VerticalMovement.started += ctx => StartMoveAxis(ctx, "Vertical");
        actionsController.PlayerAxis.VerticalMovement.performed += ctx => MovingAxis(ctx, "Vertical");
        actionsController.PlayerAxis.VerticalMovement.canceled += ctx => EndMoveAxis(ctx, "Vertical");
    }


    // Axis
    private void StartMoveAxis(InputAction.CallbackContext context, string moveType)
    {
        // Debug.Log("----------Start Horizontal Move-----------" + context);
        float inputVector = context.ReadValue<float>();
        
        // Debug.Log(inputVector);

        // ---- Can use multiple event to send in the same time ----
        OnStartMoveAxis?.Invoke(inputVector, (float)context.startTime, moveType);
    }
    
    private void MovingAxis(InputAction.CallbackContext context, string moveType)
    {
        // Debug.Log("----------Moving Axis-----------" + context);
        float inputVector = context.ReadValue<float>();
        
        // Debug.Log(inputVector);

        // ---- Can use multiple event to send in the same time ----
        OnMovingAxis?.Invoke(inputVector, (float)context.time, moveType);
    }

    private void EndMoveAxis(InputAction.CallbackContext context, string moveType)
    {
        // Debug.Log("----------End Move Axis-----------" + context);
        float inputVector = context.ReadValue<float>();
        
        // Debug.Log(inputVector);

        // ---- Can use multiple event to send in the same time ----
        OnEndMoveAxis?.Invoke(inputVector, (float)context.time, moveType);
    }

    // Movement
    private void StartMove(InputAction.CallbackContext context)
    {
        // Debug.Log("----------StartMove-----------" + context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        
        // Debug.Log(inputVector);

        // ---- Can use multiple event to send in the same time ----
        OnMovementStartWithPressPoint?.Invoke(inputVector, (float)context.startTime);
        OnMovementStart?.Invoke(
            Utils.ScreenToWorld(mainCamera, actionsController.Player.Movement.ReadValue<Vector2>()), 
            (float)context.startTime
        );
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Debug.Log("----------Moving-----------" + context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        // Debug.Log(inputVector);

        OnMovementPerformWithPressPoint?.Invoke(inputVector, (float)context.time);
        OnMovementPerform?.Invoke(
            Utils.ScreenToWorld(mainCamera, actionsController.Player.Movement.ReadValue<Vector2>()), 
            (float)context.time
        );
    }

    private void EndMove(InputAction.CallbackContext context)
    {
        // Debug.Log("----------EndMove-----------" + context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        // Debug.Log(inputVector);
        OnMovementEnd?.Invoke(
            Utils.ScreenToWorld(mainCamera, actionsController.Player.Movement.ReadValue<Vector2>()), 
            (float)context.time
        );
    }

    // Primary
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(
            Utils.ScreenToWorld(mainCamera, actionsController.Test.PrimaryPosition.ReadValue<Vector2>()), 
            (float)context.startTime
        );
    }
 
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnEndTouch != null) 
            OnEndTouch(Utils.ScreenToWorld(mainCamera, actionsController.Test.PrimaryPosition.ReadValue<Vector2>()), 
            (float)context.time
        );
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, actionsController.Test
        .PrimaryPosition.ReadValue<Vector2>());
    }
}
