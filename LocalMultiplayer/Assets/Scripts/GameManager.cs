using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
  [SerializeField] private int _roundsToWinGame = 1;
  [SerializeField] private int _timeToLoadWinScreen = 3;

  public static GameManager instance;

  public bool CanFight { get; private set; } = false;
  public bool GameWin { get; private set; } = false;
  public int RoundWinnerPlayerNumber { get; private set; } = 0;
  public Sprite WinnerSprite { get; private set; }
  
  private List<int> _roundWins = new List<int>();
  


  private void Awake()
  {
    SetUpSingleton();
  }

  public void StartRound()
  {
    CanFight = true;
    PlayerInputManager.instance.DisableJoining();

    if (_roundWins.Count != 0 && _roundWins != null) { return; }

    _roundWins = Enumerable.Repeat(0, PlayerInputManager.instance.playerCount + 1).ToList();
  }

  public void EndRound(PlayerHealth winnerPlayerHealth)
  {
    RoundWinnerPlayerNumber = winnerPlayerHealth.PlayerNumber;
    CanFight = false;
    _roundWins[RoundWinnerPlayerNumber]++;

    if (_roundWins[RoundWinnerPlayerNumber] != _roundsToWinGame) { return; }

    GameWin = true;
    WinnerSprite = winnerPlayerHealth.GetComponent<SpriteRenderer>().sprite;
    FindObjectOfType<SceneLoader>().LoadWinScreen(_timeToLoadWinScreen);

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
