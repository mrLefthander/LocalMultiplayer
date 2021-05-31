using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimations : MonoBehaviour
{
  private Animator _animator;
  private PlayerCollisionDetection _playerCollisionDetection;
  private Rigidbody2D _rigidbody;

  private void Start()
  {
    _animator = GetComponent<Animator>();
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    _animator.SetBool("isGrounded", _playerCollisionDetection.IsGrounded());
    _animator.SetFloat("ySpeed", _rigidbody.velocity.y);
    _animator.SetFloat("xSpeed", Mathf.Abs(_rigidbody.velocity.x));

    if (Mathf.Abs(_rigidbody.velocity.x) > 0)
    {
      transform.localScale = new Vector2(Mathf.Sign(_rigidbody.velocity.x), transform.localScale.y);
    }
  }
}
