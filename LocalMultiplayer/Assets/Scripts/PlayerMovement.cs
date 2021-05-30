using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private float _movementSpeed = 10f;
  [SerializeField]
  private float _jumpForce = 21f;


  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * _movementSpeed, _rigidbody.velocity.y);
  }


}
