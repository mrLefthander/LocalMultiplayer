using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;

  private float _horizontalInput;

  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);

   /* if (Input.GetButtonDown("Jump"))
    {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }*/
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    _horizontalInput = context.ReadValue<Vector2>().x;
  }

}
