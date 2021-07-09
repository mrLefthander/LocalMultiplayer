using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private int _roundsToWinGame = 1;
  public static GameManager instance;

  public bool CanFight = false;
  public bool GameWin = false;
  public int RoundWinnerPlayerNumber = 0;
  
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

  public void EndRound(int winnerPlayerNumber)
  {
    RoundWinnerPlayerNumber = winnerPlayerNumber;
    CanFight = false;
    _roundWins[RoundWinnerPlayerNumber]++;

    if (_roundWins[RoundWinnerPlayerNumber] != _roundsToWinGame) { return; }

    GameWin = true;
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
