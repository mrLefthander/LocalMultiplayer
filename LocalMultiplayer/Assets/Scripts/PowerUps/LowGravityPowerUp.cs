using UnityEngine;

public class LowGravityPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    AudioManager.instance.PlaySound(Sound.Type.PowerUpLowGravity);
    other.GetComponent<PlayerMovement>().ChangeGravityScaleForTime(PowerUpPower, PowerUpTime);
  }
}
