using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;
  [SerializeField]
  private LayerMask groundLayerMask;

  private float _horizontalInput;
  private bool _isGronded;

  private Rigidbody2D _rigidbody;
  private CapsuleCollider2D _feetCollider;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
    _feetCollider = GetComponent<CapsuleCollider2D>();
  }

  private void FixedUpdate()
  {
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
    _isGronded = IsGrounded();
  }

  

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    _horizontalInput = context.ReadValue<Vector2>().x;
  }

  public void OnJumpInput(InputAction.CallbackContext context)
  {
    if (!context.performed || !_isGronded) { return; }

    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
  }

  private bool IsGrounded()
  {
    return _feetCollider.IsTouchingLayers(groundLayerMask);
  }

}
