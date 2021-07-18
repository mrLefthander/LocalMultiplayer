using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public Sound[] sounds;

  private AudioSource audioSource;

  private void Awake()
  {
    instance = this;
    audioSource = GetComponent<AudioSource>();
  }

  public void PlaySound(Sound.Type soundName)
  {
    Sound sound = GetSoundByName(soundName);
    if (sound == null)
    {
      Debug.LogWarning("Could not find Sound " + soundName.ToString());
      return;
    }

    audioSource.PlayOneShot(sound.Clip);
    audioSource.volume = sound.Volume;
  }

  private Sound GetSoundByName(Sound.Type soundName)
  {
    return Array.Find(sounds, s => s.SoundName == soundName);
  }
}
