using UnityEngine;

public class LowGravityPowerUp: PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    other.GetComponent<PlayerMovement>().ChangeGravityScaleForTime(PowerUpPower, PowerUpTime);
  }
}
