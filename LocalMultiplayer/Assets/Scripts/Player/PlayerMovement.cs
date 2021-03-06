using UnityEngine;

[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(PlayerInputEvents))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float SMALL_JUMP_VELOCITY_MULTIPLIER = 0.3f;
  private const float JUMP_GRACE_PERIOD = 0.1f;
  private const float ON_ATTACK_MOVEMENT_PAUSE_PERIOD = 0.25f;

  [SerializeField] private float _movementSpeed = 10f;
  [SerializeField] private float _jumpForce = 21f;
  [SerializeField] private GameObject _playerSpawnEffect;

  private float _initialMovementSpeed;
  private float _initialGravityScale;
  private float _horizontalInput;
  private float _gracePeriodTimer;
  private float _wasGroundedTimer;
  private float _attackPauseTimer;
  private float _movementSpeedChangeTimer;
  private float _gravityScaleTimer;


  private PlayerCollisionDetection _playerCollisionDetection;
  private PlayerInputEvents _playerInputEvents;

  private Rigidbody2D _rigidbody;

  private void Awake()
  {
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
    _playerInputEvents = GetComponent<PlayerInputEvents>();

    _rigidbody = GetComponent<Rigidbody2D>();

    _initialMovementSpeed = _movementSpeed;
    _initialGravityScale = _rigidbody.gravityScale;

    DontDestroyOnLoad(gameObject);
  }
  private void OnEnable()
  {
    Instantiate(_playerSpawnEffect, transform.position, Quaternion.identity);
    AudioManager.instance.PlaySound(Sound.Type.Portal);
    SubscribeForEvents();
  }
  
  private void Update()
  {
    CountDownTimer(ref _gracePeriodTimer);
    CountDownTimer(ref _wasGroundedTimer);
    CountDownTimer(ref _attackPauseTimer);
    CountDownTimer(ref _movementSpeedChangeTimer);
    CountDownTimer(ref _gravityScaleTimer);

    ReturnToInitialMovementSpeedOnTimerEnd();
    ReturnToInitialGravityScaleOnTimerEnd();

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

  private void CountDownTimer(ref float timer)
  {
    if (timer <= 0f)
      return;
    timer -= Time.deltaTime;
  }

  public float GetVelocityY()
  {
    return _rigidbody.velocity.y;
  }

  public void OnJump()
  {
    if (GameManager.instance.IsPaused) { return; }
    _gracePeriodTimer = JUMP_GRACE_PERIOD;
  }
  private void Jump()
  {
    if (_gracePeriodTimer <= 0f || _wasGroundedTimer <= 0f) { return; }
   
    AudioManager.instance.PlaySound(Sound.Type.Bounce);
    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    _gracePeriodTimer = 0f;
    _wasGroundedTimer = 0f;
  }

  public void OnJumpCanceled()
  {
    if (_rigidbody.velocity.y < 0f) { return; }

    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * SMALL_JUMP_VELOCITY_MULTIPLIER);
  }

  private void OnMove(float x)
  {
    _horizontalInput = x;
  }

  private void Move()
  {
    _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
  }

  public void ChangeMovementSpeedForTime(float movementSpeed, float time)
  {
    _movementSpeed = movementSpeed;
    _movementSpeedChangeTimer = time;
  }
  private void ReturnToInitialMovementSpeedOnTimerEnd()
  {
    if (_movementSpeedChangeTimer <= 0f && _movementSpeed != _initialMovementSpeed)
      _movementSpeed = _initialMovementSpeed;
  }

  public void ChangeGravityScaleForTime(float gravityScale, float time)
  {
    _rigidbody.gravityScale = gravityScale;
    _gravityScaleTimer = time;
  }
  private void ReturnToInitialGravityScaleOnTimerEnd()
  {
    if (_gravityScaleTimer <= 0f && _rigidbody.gravityScale != _initialGravityScale)
      _rigidbody.gravityScale = _initialGravityScale;
  }

  private void OnAttack()
  {
    if (GameManager.instance.IsPaused) { return; }

    AudioManager.instance.PlaySound(Sound.Type.Attack);
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
