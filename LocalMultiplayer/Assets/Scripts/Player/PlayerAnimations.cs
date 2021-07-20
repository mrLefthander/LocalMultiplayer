using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerCollisionDetection))]
[RequireComponent(typeof(PlayerInputEvents))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimations : MonoBehaviour
{
  private PlayerInputEvents _playerInputEvents;
  private PlayerCollisionDetection _playerCollisionDetection;

  private Rigidbody2D _rigidbody;
  private Animator _animator;

  private void Awake()
  {
    _playerCollisionDetection = GetComponent<PlayerCollisionDetection>();   
    _playerInputEvents = GetComponent<PlayerInputEvents>();

    _animator = GetComponent<Animator>();
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  private void OnEnable()
  {
    _playerInputEvents.AttackEvent += OnAttackAnimation;
    _playerInputEvents.MoveEvent += OnMoveAnimation;
  }

  private void Update()
  {
    if (GameManager.instance.IsPaused) 
    {
      _animator.StopPlayback();
    }
   // _animator.StartPlayback();
    _animator.SetBool(ApplicationVariables.AnimationNames.IsGrounded, _playerCollisionDetection.IsGrounded);
    _animator.SetFloat(ApplicationVariables.AnimationNames.YSpeed, _rigidbody.velocity.y);
  }

  private void OnDisable()
  {
    _playerInputEvents.AttackEvent -= OnAttackAnimation;
    _playerInputEvents.MoveEvent -= OnMoveAnimation;
  }

  private void OnAttackAnimation()
  {
    if (GameManager.instance.IsPaused) { return; }
    
    _animator.SetTrigger(ApplicationVariables.AnimationNames.Attack);
  }

  private void OnMoveAnimation(float dirrection)
  {
    if (GameManager.instance.IsPaused) { return; }
    _animator.SetFloat(ApplicationVariables.AnimationNames.XSpeed, Mathf.Abs(dirrection));
    FlipSprite(dirrection);
  }

  private void FlipSprite(float dirrection)
  {
    if (Mathf.Abs(dirrection) > 0)
      transform.localScale = new Vector2(Mathf.Sign(dirrection) * 1f, transform.localScale.y);
  }

  public void PlayerInvincibleAnimation(bool value)
  {
    _animator.SetBool(ApplicationVariables.AnimationNames.IsInvincible, value);
  }

  public void ChangeToPlayerAnimatorOverrideController(AnimatorOverrideController animatorOverride)
  {
    _animator.runtimeAnimatorController = animatorOverride;
  }
}
