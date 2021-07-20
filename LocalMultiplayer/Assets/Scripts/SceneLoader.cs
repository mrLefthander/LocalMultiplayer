using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
  private IArenaLoadTrigger _arenaLoadTrigger;
  private IEnumerator sceneLoadCoroutine;
  private bool isStoped;

  private void Awake()
  {
   _arenaLoadTrigger = FindObjectsOfType<MonoBehaviour>().OfType<IArenaLoadTrigger>().SingleOrDefault();
  }

  private void OnEnable()
  {
    if(GameManager.instance != null)
      GameManager.instance.GameEndEvent += LoadWinScreen;

    if (_arenaLoadTrigger == null) { return; }

    _arenaLoadTrigger.ArenaLoadEvent += OnArenaLoad;
    _arenaLoadTrigger.ArenaLoadCanceledEvent += OnArenaLoadCanceled;
  }

  private void OnDisable()
  {
    if (GameManager.instance != null)
      GameManager.instance.GameEndEvent -= LoadWinScreen;

    if (_arenaLoadTrigger == null) { return; }

    _arenaLoadTrigger.ArenaLoadEvent -= OnArenaLoad;
    _arenaLoadTrigger.ArenaLoadCanceledEvent -= OnArenaLoadCanceled;
  }

  public void LoadLevel(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void LoadLevelWithDelay(string sceneName, int delayTime)
  {
    isStoped = false;
    sceneLoadCoroutine = DelayedLoad(sceneName, delayTime);
    StartCoroutine(sceneLoadCoroutine);
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

  public void OnArenaLoad(int delayTime)
  {
    LoadLevelWithDelay(ApplicationVariables.SceneNames.GetNextArenaName(), delayTime);
  }

  public void OnArenaLoadCanceled()
  {
    isStoped = true;
    StopCoroutine(sceneLoadCoroutine);
  }

  public void LoadWinScreen(int delayTime)
  {
    LoadLevelWithDelay(ApplicationVariables.SceneNames.WinScreen, delayTime);
  }
  
  public void RestartGame()
  {
    GameManager.instance?.DestroyGameManager();
    LoadLevel(ApplicationVariables.SceneNames.CharacterSelection);
  }

  public void LoadMainMenu()
  {
    FindObjectOfType<ArenaConfiguration>()?.DestroyAllPlayers();
    GameManager.instance?.DestroyGameManager();
    LoadLevel(ApplicationVariables.SceneNames.MainMenu);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
