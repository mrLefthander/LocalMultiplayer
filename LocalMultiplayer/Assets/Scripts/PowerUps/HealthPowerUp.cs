using UnityEngine;

public class HealthPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    other.GetComponent<PlayerHealth>().ResetHealth();
  }
}
