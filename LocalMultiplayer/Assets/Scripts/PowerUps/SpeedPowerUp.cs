using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
  protected override void PickUpPower(Collider2D other)
  {
    AudioManager.instance.PlaySound(Sound.Type.PowerUpSpeed);
    other.GetComponent<PlayerMovement>().ChangeMovementSpeedForTime(PowerUpPower, PowerUpTime);
  }
}
