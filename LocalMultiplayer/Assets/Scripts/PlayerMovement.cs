using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float SMALL_JUMP_MULTIPLIER = 0.3f;
  private const float JUMP_GRACE_PERIOD = 0.1f;

  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;
  
  private float _horizontalInput;
  private float _gracePeriodTimer;
  private float _wasGroundedTimer;

  private PlayerCollisionDetection _playerCollisionDetection;
  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _gracePeriodTimer -= Time.deltaTime;    
    _wasGroundedTimer -= Time.deltaTime;
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);

    if (_playerCollisionDetection.IsGrounded())
      _wasGroundedTimer = JUMP_GRACE_PERIOD;

    Jump();
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    _horizontalInput = context.ReadValue<Vector2>().x;
  }

  public void OnJumpInput(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      _gracePeriodTimer = JUMP_GRACE_PERIOD;
    }

    if (!context.canceled || _rigidbody.velocity.y <= 0f)
    {
      return;
    }

    SmallJump();
  }

  private void Jump()
  {
    if (_gracePeriodTimer > 0f && _wasGroundedTimer > 0f)
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
      _gracePeriodTimer = 0f;
      _wasGroundedTimer = 0f;
    }
  }

  private void SmallJump()
  {
    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * SMALL_JUMP_MULTIPLIER);
  }

}
