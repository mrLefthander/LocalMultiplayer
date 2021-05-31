using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float COLLIDER_SIZE_MULTIPLIER_FOR_GROUND_CHECK = 0.98f;
  private const float BOX_CAST_ANGLE = 0f;
  private const float SMALL_JUMP_MULTIPLIER = 0.3f;

  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;


  [SerializeField]
  private LayerMask groundLayerMask;
  private bool _isGronded;
  private float _horizontalInput;


  private Rigidbody2D _rigidbody;
  private Collider2D _collider;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
  }

  private void FixedUpdate()
  {
    MoveHorizontaly();
    _isGronded = IsGrounded();
  }

  private void MoveHorizontaly()
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
    if (context.performed && _isGronded)
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
  }

  private void SmallJump(InputAction.CallbackContext context)
  {
    if (context.canceled && !_isGronded && _rigidbody.velocity.y > 0f)
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * SMALL_JUMP_MULTIPLIER);
    }
  }

  private bool IsGrounded()
  {
    RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size * COLLIDER_SIZE_MULTIPLIER_FOR_GROUND_CHECK, BOX_CAST_ANGLE, 
      Vector2.down, _collider.bounds.extents.x, groundLayerMask);

    return raycastHit.collider != null;
  }

}
