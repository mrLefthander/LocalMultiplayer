using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public bool canFight = true;
  public int startGameContDownTime = 3;


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
