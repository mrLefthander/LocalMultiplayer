using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
  private const string CHARACTER_SELECTION_SCENE_NAME = "CharacterSelection";
  private const string GAMEPLAY_SCENE_NAME = "ArenaTest1";

  private GameStartChecker _gameStartChecker;
  private IEnumerator gameStartCoroutine;
  private bool isStoped;

  private void Awake()
  {
    if(SceneManager.GetActiveScene().name == CHARACTER_SELECTION_SCENE_NAME)
      _gameStartChecker = FindObjectOfType<GameStartChecker>();
  }

  private void OnEnable()
  {
    if (_gameStartChecker != null)
    {
      _gameStartChecker.GameStartEvent += OnGameStart;
      _gameStartChecker.GameStartCanceledEvent += OnGameStartCanceled;
    }
  }

  private void OnDisable()
  {
    if (_gameStartChecker != null)
    {
      _gameStartChecker.GameStartEvent -= OnGameStart;
      _gameStartChecker.GameStartCanceledEvent -= OnGameStartCanceled;
    }
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void LoadLevelWithDelay(string sceneName, int delayTime)
  {
    isStoped = false;
    gameStartCoroutine = DelayedLoad(sceneName, delayTime);
    StartCoroutine(gameStartCoroutine);
  }

  IEnumerator DelayedLoad(string sceneName, int delayTime)
  {
    for (int i = delayTime; i >= 0; i--)
    {
      yield return new WaitForSeconds(1f);
    }

    if(!isStoped)
      LoadLevel(sceneName);
  }

  public void LoadLevel(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }
  
  public void OnGameStart(int delayTime)
  {
    LoadLevelWithDelay(GAMEPLAY_SCENE_NAME, delayTime);
  }

  public void OnGameStartCanceled()
  {
    isStoped = true;
    StopCoroutine(gameStartCoroutine);
  }
}
