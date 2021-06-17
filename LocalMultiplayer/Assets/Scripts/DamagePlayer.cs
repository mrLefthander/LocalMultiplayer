using UnityEngine;

public class DamagePlayer: MonoBehaviour
{
  [SerializeField] private int _damageToDeal = 1;
  [SerializeField] private LayerMask _playerLayerMask;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!IsTouchingPlayer(other)) { return; }

    other.GetComponent<PlayerHealth>().TakeDamage(_damageToDeal);
  }

  private bool IsTouchingPlayer(Collider2D other)
  {
    return _playerLayerMask == (_playerLayerMask | (1 << other.gameObject.layer));
  }
}

