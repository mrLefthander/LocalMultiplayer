using UnityEngine;

[System.Serializable]
public class Sound 
{
  public enum Type
  {
    Attack,
    Bounce,
    ButtonPress,
    Countdown,
    Death,
    Hit,
    JumpPad,
    Portal,
    PowerUpHealth,
    PowerUpSpeed,
    PowerUpInvincibility,
    PowerUpLowGravity
  }

  public Sound.Type SoundName;
  public AudioClip Clip;

  [Range(0f, 1f)]
  public float Volume = 1f;
}
