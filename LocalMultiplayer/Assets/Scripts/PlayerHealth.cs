using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  private readonly int _maxHealth = 3;
  private int _currentHealth;

  [SerializeField] private Transform _heartsHolder;
  [SerializeField] private SpriteRenderer[] _heartSprites;
  [SerializeField] private Sprite _heartFull;
  [SerializeField] private Sprite _heartEmpty;

  private void Start()
  {
    _currentHealth = _maxHealth;
  }

  private void LateUpdate()
  {
    _heartsHolder.localScale = transform.localScale;
  }

  public void UpdateDisplay()
  {
    for(int i = 0; i < _heartSprites.Length; i++)
    {
      _heartSprites[i].sprite = _heartFull;

      if(i >= _currentHealth)
        _heartSprites[i].sprite = _heartEmpty;
    }
  }

  public void TakeDamage(int amount)
  {
    _currentHealth -= amount;
    UpdateDisplay();
    if (_currentHealth < 0)
    {
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
  }
}
