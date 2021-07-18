using UnityEngine;

public class InvincibilityPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    other.GetComponent<PlayerHealth>().MakeInvincible(PowerUpTime);
  }
}
