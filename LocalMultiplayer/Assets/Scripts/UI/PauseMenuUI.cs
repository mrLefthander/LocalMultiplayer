using UnityEngine;
using UnityEngine.EventSystems;


public class PauseMenuUI : MonoBehaviour
{
  [SerializeField] private GameObject _pauseMenuContainer;
  [SerializeField] private GameObject _firstPauseButton;

  private PauseAction _pauseAction;

  private void Awake()
  {
    _pauseAction = new PauseAction();
    _pauseAction.Enable();
    _pauseMenuContainer.gameObject.SetActive(false);
  }

  private void Start()
  {
    _pauseAction.Pause.PauseGame.performed += _ => PauseUnpause();
  }

  private void OnEnable()
  {
    _pauseAction.Enable();
  }

  private void OnDisable()
  {
    _pauseAction.Disable();
  }

  private void OnDestroy()
  {
    _pauseAction.Dispose();
  }

  private void PauseUnpause()
  {
    if (_pauseMenuContainer.gameObject.activeInHierarchy)
    {
      ResumeGame();
    }
    else
    {
      PauseGame();
    }
  }

  private void PauseGame()
  {
    GameManager.instance.PauseGame();
    _pauseMenuContainer.gameObject.SetActive(true);
    EventSystem.current.SetSelectedGameObject(null);
    EventSystem.current.SetSelectedGameObject(_firstPauseButton);
  }

  public void ResumeGame()
  {
    GameManager.instance.ResumeGame();
    _pauseMenuContainer.gameObject.SetActive(false);
  }
}
