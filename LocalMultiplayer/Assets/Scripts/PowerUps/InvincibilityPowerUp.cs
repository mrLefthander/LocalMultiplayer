using UnityEngine;

public class InvincibilityPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    AudioManager.instance.PlaySound(Sound.Type.PowerUpInvincibility);
    other.GetComponent<PlayerHealth>().MakeInvincible(PowerUpTime);
  }
}
