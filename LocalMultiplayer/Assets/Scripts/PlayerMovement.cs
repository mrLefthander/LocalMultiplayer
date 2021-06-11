using UnityEngine;

[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(PlayerInputEvents))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float SMALL_JUMP_MULTIPLIER = 0.3f;
  private const float JUMP_GRACE_PERIOD = 0.15f;
  private const float ON_ATTACK_MOVEMENT_PAUSE_PERIOD = 0.25f;

  [SerializeField] private float _movementSpeed = 10f;
  [SerializeField] private float _jumpForce = 21f;
  [SerializeField] private GameObject _playerSpawnEffect;

  private float _horizontalInput;
  private float _gracePeriodTimer;
  private float _wasGroundedTimer;
  private float _attackPauseTimer;

  private PlayerCollisionDetection _playerCollisionDetection;
  private PlayerInputEvents _playerInputEvents;
  private Rigidbody2D _rigidbody;

  private void Awake()
  {
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
    _rigidbody = GetComponent<Rigidbody2D>();
    _playerInputEvents = GetComponent<PlayerInputEvents>();
  }
  private void OnEnable()
  {
    Instantiate(_playerSpawnEffect, transform.position, Quaternion.identity);
    SubscribeForEvents();
  }
  
  private void Update()
  {
    _gracePeriodTimer -= Time.deltaTime;
    _wasGroundedTimer -= Time.deltaTime;
    _attackPauseTimer -= Time.deltaTime;

    if (_playerCollisionDetection.IsGrounded)
      _wasGroundedTimer = JUMP_GRACE_PERIOD;

    Jump();
    Move();
    StopOnAttack();
  }

  private void OnDisable()
  {
    UnsubscribeFromEvents();
  }

  public void OnJump()
  {
    _gracePeriodTimer = JUMP_GRACE_PERIOD;
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

  public void OnJumpCanceled()
  {
    if (_rigidbody.velocity.y >= 0f)
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * SMALL_JUMP_MULTIPLIER);
  }

  private void OnMove(float x)
  {
    _horizontalInput = x;
  }

  private void Move()
  {
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
  }

  private void OnAttack()
  {
    _attackPauseTimer = ON_ATTACK_MOVEMENT_PAUSE_PERIOD;
  }

  private void StopOnAttack()
  {
    if (_attackPauseTimer > 0f)
      _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
  }

  private void SubscribeForEvents()
  {
    _playerInputEvents.MoveEvent += OnMove;
    _playerInputEvents.JumpEvent += OnJump;
    _playerInputEvents.JumpCanceledEvent += OnJumpCanceled;
    _playerInputEvents.AttackEvent += OnAttack;
  }

  private void UnsubscribeFromEvents()
  {
    _playerInputEvents.MoveEvent -= OnMove;
    _playerInputEvents.JumpEvent -= OnJump;
    _playerInputEvents.JumpCanceledEvent -= OnJumpCanceled;
    _playerInputEvents.AttackEvent -= OnAttack;
  }
}
