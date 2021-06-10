using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  private void Awake()
  {
    SetUpSingleton();
  }

  private void SetUpSingleton()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      gameObject.SetActive(false);
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);
  }
}
