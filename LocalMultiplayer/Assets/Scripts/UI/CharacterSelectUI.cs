using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelectUI : MonoBehaviour
{
  [SerializeField] private GameObject _joinTextGO;
  [SerializeField] private TMP_Text _countDownText;
  [SerializeField] private GameStartChecker _gameStartChecker;

  private IEnumerator gameStartCoroutine;

  private void OnEnable()
  {
    _gameStartChecker.ArenaLoadEvent += OnGameStart;
    _gameStartChecker.ArenaLoadCanceledEvent += OnGameStartCanceled;
  }

  private void Update()
  {
    _joinTextGO.SetActive(!_gameStartChecker.IsMaxPlayers());
  }

  private void OnDisable()
  {
    _gameStartChecker.ArenaLoadEvent -= OnGameStart;
  }

  public void OnGameStart(int delayTime)
  {
    _countDownText.gameObject.SetActive(true);
    gameStartCoroutine = CountDownStartCounter(delayTime);
    StartCoroutine(gameStartCoroutine);
  }
  public void OnGameStartCanceled()
  {
    StopCoroutine(gameStartCoroutine);
    _countDownText.gameObject.SetActive(false);
  }

  IEnumerator CountDownStartCounter(int timeToCount)
  {
    for(int i = timeToCount; i >= 0; i--)
    {
      _countDownText.text = i.ToString();
      yield return new WaitForSeconds(1f);
    }
  }
}
