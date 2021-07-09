using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ApplicationVariables
{
  public static class SceneNames
  {
    public static readonly string CharacterSelection = "CharacterSelection";
    public static readonly string[] ArenaLevels = {"ArenaTest1", "Player Testing"};
    private static readonly List<string> _arenasToLoad = new List<string>(ArenaLevels);

    public static string GetNextArenaName()
    {
      if (_arenasToLoad.Count == 0)
      {
        _arenasToLoad.AddRange(ArenaLevels);
      }

      string randomLevelName = _arenasToLoad[Random.Range(0, _arenasToLoad.Count)];
      _arenasToLoad.Remove(randomLevelName);
      
      return randomLevelName;
    }
  }

  public static class AnimationNames
  {
    public static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    public static readonly int IsInvincible = Animator.StringToHash("isInvincible");
    public static readonly int YSpeed = Animator.StringToHash("ySpeed");
    public static readonly int XSpeed = Animator.StringToHash("xSpeed");
    public static readonly int Attack = Animator.StringToHash("attack");
  }
}
