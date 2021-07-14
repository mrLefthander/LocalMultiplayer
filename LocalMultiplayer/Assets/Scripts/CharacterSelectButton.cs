using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
  private const float BUTTON_POPUP_TIME = 0.5f;
  private const float MINIMAL_VELOCITY_FOR_IN_JUMP_CHECK = -0.1f;

  [SerializeField] private Sprite _buttonUp;
  [SerializeField] private Sprite _buttonDown;
  [SerializeField] private AnimatorOverrideController _animatorOverrideController;

  private bool _isPressed;
  private float _buttonPopUpTimer;

  private SpriteRenderer _spriteRenderer;

  private void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    CountDownTimer(ref _buttonPopUpTimer);
    ChangeIsPressedOnTimerEnd();
    UpdateButtonSprite(_isPressed);
  }

  private void UpdateButtonSprite(bool isPressed)
  {
    if (isPressed)
      _spriteRenderer.sprite = _buttonDown;
    else
      _spriteRenderer.sprite = _buttonUp;
  }

  private void ChangeIsPressedOnTimerEnd()
  {
    if (_buttonPopUpTimer > 0f) { return; }

    _isPressed = false;
  }

  private void CountDownTimer(ref float timer)
  {
    if (!_isPressed) { return; }
    timer -= Time.deltaTime;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer) || _isPressed) { return; }

    float playerVelocityY = other.GetComponent<PlayerMovement>().GetVelocityY();

    if (playerVelocityY >= MINIMAL_VELOCITY_FOR_IN_JUMP_CHECK) { return; }

    _isPressed = true;
    _buttonPopUpTimer = BUTTON_POPUP_TIME;
    other.GetComponent<PlayerAnimations>().ChangeToPlayerAnimatorOverrideController(_animatorOverrideController);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    _isPressed = false;
  }
}
