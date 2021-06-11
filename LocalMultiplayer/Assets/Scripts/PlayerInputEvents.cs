using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputEvents : MonoBehaviour
{
  public event UnityAction<float> MoveEvent = delegate { };
  public event UnityAction JumpEvent = delegate { };
  public event UnityAction JumpCanceledEvent = delegate { };
  public event UnityAction AttackEvent = delegate { };

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    MoveEvent.Invoke(context.ReadValue<Vector2>().x);
  }

  public void OnJumpInput(InputAction.CallbackContext context)
  {
    if (context.performed)
      JumpEvent.Invoke();

    if (context.canceled)
      JumpCanceledEvent.Invoke();
  }

  public void OnAttackInput(InputAction.CallbackContext context)
  {
    if(context.performed)
      AttackEvent.Invoke();
  }
}
