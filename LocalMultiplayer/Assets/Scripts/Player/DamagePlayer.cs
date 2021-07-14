using UnityEngine;

public class DamagePlayer: MonoBehaviour
{
  [SerializeField] private int _damageToDeal = 1;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer) || !GameManager.instance.CanFight) { return; }

    other.GetComponent<PlayerHealth>().TakeDamage(_damageToDeal);
  }
}

