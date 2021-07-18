using UnityEngine;
using UnityEngine.Events;

public abstract class PowerUp : MonoBehaviour
{
  [SerializeField] private GameObject _powerUpEffect;
  [SerializeField] protected float PowerUpTime;
  [SerializeField] protected float PowerUpPower;

  [HideInInspector]
  public SpawnPoint OccupiedSpawnPoint { get; set; }

  public event UnityAction<PowerUp> PickUpEvent;

  protected abstract void PickUpPower(Collider2D other);

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer))
    {
      PickUpPower(other);
      OnPickUp();
    }
  }

  private void OnPickUp()
  {
    Instantiate(_powerUpEffect, transform.position, Quaternion.identity);
    PickUpEvent?.Invoke(this);
    Destroy(gameObject);
  }
}
