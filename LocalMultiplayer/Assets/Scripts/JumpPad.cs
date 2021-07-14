using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class JumpPad : MonoBehaviour
{
  private const float BUTTON_POPDOWN_TIME = 0.25f;

  [SerializeField] private Sprite _downSprite;
  [SerializeField] private Sprite _upSprite;
  [SerializeField] private float _boucePower;

  private float _buttonPopDownTimer;

  private SpriteRenderer _spriteRenderer;

  private void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    CountDownTimer(ref _buttonPopDownTimer);
    ChangeToDownSpriteOnTimerEnd();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer)) { return; }

    _spriteRenderer.sprite = _upSprite;
    _buttonPopDownTimer = BUTTON_POPDOWN_TIME;

    Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _boucePower);
  }

  private void ChangeToDownSpriteOnTimerEnd()
  {
    if (_buttonPopDownTimer > 0f) { return; }

    _spriteRenderer.sprite = _downSprite;
  }

  private void CountDownTimer(ref float timer)
  {
    timer -= Time.deltaTime;
  }
}
