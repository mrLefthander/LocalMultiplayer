using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
  [SerializeField] private int _roundsToWinGame = 2;
  [SerializeField] private int _timeToLoadWinScreen = 3;


  public static GameManager instance;

  public bool IsPaused { get; private set; } = false;
  public bool CanFight { get; private set; } = false;
  public bool GameWin { get; private set; } = false;
  public int RoundWinnerPlayerNumber { get; private set; } = 0;
  public Sprite WinnerSprite { get; private set; } = null;
  public event UnityAction<int> GameEndEvent;

  private List<int> _roundWins = new List<int>();
  
  private void Awake()
  {
    SetUpSingleton();
    ResumeGame();
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

    EndGame(winnerPlayerHealth);
  }

  private void EndGame(PlayerHealth winnerPlayerHealth)
  {
    GameWin = true;
    WinnerSprite = winnerPlayerHealth.GetComponent<SpriteRenderer>().sprite;
    GameEndEvent?.Invoke(_timeToLoadWinScreen);
    //FindObjectOfType<SceneLoader>().LoadWinScreen(_timeToLoadWinScreen);
  }

  public void PauseGame()
  {
    IsPaused = true;
    AudioListener.pause = true;
    Time.timeScale = 0f;
  }

  public void ResumeGame()
  {
    IsPaused = false;
    AudioListener.pause = false;
    Time.timeScale = 1f;
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

  public void DestroyGameManager()
  {
    Destroy(PlayerInputManager.instance.gameObject);

    instance = null;
    Destroy(gameObject);
  }
}
