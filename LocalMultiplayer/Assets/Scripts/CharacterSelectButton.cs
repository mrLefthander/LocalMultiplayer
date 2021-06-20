using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterSelectButton : MonoBehaviour
{
  private const float BUTTON_POPUP_TIME = 0.5f;

  [SerializeField] private Sprite _buttonUp, _buttonDown;
  [SerializeField] private LayerMask _playerLayerMask;

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
    bool isTriggeredByPlayer = _playerLayerMask == (_playerLayerMask | (1 << other.gameObject.layer));

    if (!isTriggeredByPlayer || _isPressed) { return; }

    float playerVelocityY = other.GetComponent<PlayerMovement>().GetVelocityY();

    if (playerVelocityY >= -0.1f) { return; }

    _isPressed = true;
    _buttonPopUpTimer = BUTTON_POPUP_TIME;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    _isPressed = false;
  }
}
