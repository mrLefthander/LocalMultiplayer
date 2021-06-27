using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerAnimations))]
public class PlayerHealth : MonoBehaviour
{
  private const float INVINCIBILITY_TIME = 1f;
  private const int DEFAULT_DAMAGE_TO_TAKE = 1;

  [SerializeField] private Transform _heartsHolder;
  [SerializeField] private SpriteRenderer[] _heartSprites;
  [SerializeField] private Sprite _heartFull;
  [SerializeField] private Sprite _heartEmpty;

  private readonly int _maxHealth = 3;
  private int _currentHealth;
  private float _invincibilityTimer;

  private PlayerAnimations _playerAnimations;

  private void Start()
  {
    ResetHealth();
    _playerAnimations = GetComponent<PlayerAnimations>();
    SceneManager.sceneLoaded += OnSceneChange;
  }

  private void Update()
  {
    InvincibilityStateTimer();
  }

  private void LateUpdate()
  {
    _heartsHolder.localScale = transform.localScale;
  }

  private void OnDisable()
  {
    SceneManager.sceneLoaded -= OnSceneChange;
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
    _invincibilityTimer = INVINCIBILITY_TIME;
    UpdateDisplay();

    if (_currentHealth >= 0) { return; }

    gameObject.SetActive(false);
    Destroy(gameObject);
  }

  public void ResetHealth()
  {
    _currentHealth = _maxHealth;
  }

  private void OnSceneChange(Scene scene, LoadSceneMode mode)
  {
    ResetHealth();
    UpdateDisplay();
  }
}
