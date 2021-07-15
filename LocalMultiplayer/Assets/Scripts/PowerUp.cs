using UnityEngine;

public class PowerUp : MonoBehaviour
{
  [SerializeField] private GameObject _powerUpEffect;
  public bool isHealth;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer))
    {
      if (isHealth)
      {
        other.GetComponent<PlayerHealth>().ResetHealth();
      }

      Instantiate(_powerUpEffect, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
    
  }
}
