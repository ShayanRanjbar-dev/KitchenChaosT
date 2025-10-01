using UnityEngine;
using System;
public class InputScript : MonoBehaviour
{
    public static InputScript instance { get; private set; }
    public event EventHandler OnPauseAction;
    public event EventHandler OnInterAction;
    public event EventHandler OnAlternativeInteractAction;
    private PlayerInput inputActions;
    private void Awake()
    {
        instance = this;
        inputActions = new PlayerInput();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.AlternativeInteract.performed += AlternativeInteract_performed;
        inputActions.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        inputActions.Player.Interact.performed -= Interact_performed;
        inputActions.Player.AlternativeInteract.performed -= AlternativeInteract_performed;
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    private void AlternativeInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAlternativeInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInterAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        
        Vector2 moveVector = inputActions.Player.Move.ReadValue<Vector2>();
        moveVector = moveVector.normalized;
        return moveVector;
    }
    }
