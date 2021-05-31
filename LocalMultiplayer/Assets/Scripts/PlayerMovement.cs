using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float SMALL_JUMP_MULTIPLIER = 0.3f;

  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;
  
  private float _horizontalInput;

  private PlayerCollisionDetection _playerCollisionDetection;
  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    _horizontalInput = context.ReadValue<Vector2>().x;
  }

  public void OnJumpInput(InputAction.CallbackContext context)
  {
    Jump(context);

    SmallJump(context);
  }

  private void Jump(InputAction.CallbackContext context)
  {
    if (context.performed && _playerCollisionDetection.IsGrounded())
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
  }

  private void SmallJump(InputAction.CallbackContext context)
  {
    if (context.canceled && !_playerCollisionDetection.IsGrounded() && _rigidbody.velocity.y > 0f)
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * SMALL_JUMP_MULTIPLIER);
    }
  }

}
