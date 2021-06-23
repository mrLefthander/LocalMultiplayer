using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
  private const string CHARACTER_SELECTION_SCENE_NAME = "CharacterSelection";
  private const string GAMEPLAY_SCENE_NAME = "Player Testing";

  private GameStartChecker _gameStartChecker;
  
  private void OnEnable()
  {
    _gameStartChecker.GameStartEvent += OnGameStart;
  }

  private void Awake()
  {
    if(SceneManager.GetActiveScene().name == CHARACTER_SELECTION_SCENE_NAME)
      _gameStartChecker = FindObjectOfType<GameStartChecker>();

    if (_gameStartChecker != null)
      _gameStartChecker.GameStartEvent += OnGameStart;
  }

  private void OnDisable()
  {
    if (_gameStartChecker != null)
      _gameStartChecker.GameStartEvent += OnGameStart;
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void LoadLevelWithDelay(string sceneName, int delayTime)
  {
    StartCoroutine(DelayedLoad(sceneName, delayTime));
  }

  IEnumerator DelayedLoad(string sceneName, int delayTime = 0)
  {
    for (int i = delayTime; i >= 0; i--)
    {
      yield return new WaitForSeconds(1f);
    }
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
}
