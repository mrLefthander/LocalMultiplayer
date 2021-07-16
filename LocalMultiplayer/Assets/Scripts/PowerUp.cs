using UnityEngine;

public class PowerUp : MonoBehaviour
{
  [SerializeField] private GameObject _powerUpEffect;
  [SerializeField] private float _powerUpTime;
  [SerializeField] private float _powerUpPower;
  [SerializeField] private bool _isHealth;
  [SerializeField] private bool _isInvincible;
  [SerializeField] private bool _isSpeed;
  

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer))
    {
      if (_isHealth)
      {
        other.GetComponent<PlayerHealth>().ResetHealth();
      }

      if (_isInvincible)
      {
        other.GetComponent<PlayerHealth>().MakeInvincible(_powerUpTime);
      }

      if (_isSpeed)
      {
        other.GetComponent<PlayerMovement>().ChangeMovementSpeedForTime(_powerUpPower, _powerUpTime);
      }

      Instantiate(_powerUpEffect, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
    
  }
}
