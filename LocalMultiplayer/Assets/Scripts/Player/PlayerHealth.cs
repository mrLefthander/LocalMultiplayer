using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerHealth : MonoBehaviour
{
  private const float INVINCIBILITY_TIME = 1f;
  private const int DEFAULT_DAMAGE_TO_TAKE = 1;

  [SerializeField] private Transform _heartsHolder;
  [SerializeField] private SpriteRenderer[] _heartSprites;
  [SerializeField] private Sprite _heartFull;
  [SerializeField] private Sprite _heartEmpty;

  public int PlayerNumber { get; private set; }
  public event UnityAction<PlayerHealth> DeathEvent = delegate { };
  private readonly int _maxHealth = 3;
  private int _currentHealth;
  private float _invincibilityTimer;

  private PlayerAnimations _playerAnimations;

  private void Start()
  {
    PlayerNumber = PlayerInputManager.instance.playerCount;
    ResetHealth();
    _playerAnimations = GetComponent<PlayerAnimations>();
  }

  private void Update()
  {
    InvincibilityStateTimer();
  }

  private void LateUpdate()
  {
    _heartsHolder.localScale = transform.localScale;
  }

  private void InvincibilityStateTimer()
  {
    if (_invincibilityTimer < 0f) { return; }

    _invincibilityTimer -= Time.deltaTime;
    _playerAnimations.PlayerInvincibleAnimation(_invincibilityTimer > 0f);
  }

  public void UpdateDisplay()
  {
    for(int i = 0; i < _heartSprites.Length; i++)
    {
      if (i < _currentHealth)
        _heartSprites[i].sprite = _heartFull;
      else
        _heartSprites[i].sprite = _heartEmpty;
    }
  }

  public void TakeDamage(int amount = DEFAULT_DAMAGE_TO_TAKE)
  {
    if (_invincibilityTimer > 0f) { return; }

    _currentHealth -= amount;
    AudioManager.instance.PlaySound(Sound.Type.Hit);
    MakeInvincible(INVINCIBILITY_TIME);
    UpdateDisplay();

    if (_currentHealth >= 0) { return; }

    AudioManager.instance.PlaySound(Sound.Type.Death);
    gameObject.SetActive(false);
    DeathEvent?.Invoke(this);
  }

  public void MakeInvincible(float invincibilityTime)
  {
    _invincibilityTimer = invincibilityTime;
  }

  public void ResetHealth()
  {
    gameObject.SetActive(true);
    _currentHealth = _maxHealth;
    UpdateDisplay();
  }
}
