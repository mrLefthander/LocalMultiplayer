using UnityEngine;

public class HealthPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    AudioManager.instance.PlaySound(Sound.Type.PowerUpHealth);
    other.GetComponent<PlayerHealth>().ResetHealth();
  }
}
