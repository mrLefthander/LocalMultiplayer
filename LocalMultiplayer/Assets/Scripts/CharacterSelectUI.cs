using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelectUI : MonoBehaviour
{
  [SerializeField] private GameObject _joinTextGO;
  [SerializeField] private TMP_Text _countDownText;
  [SerializeField] private PlayerInputManager _playerInputManager;
  [SerializeField] private GameStartChecker _gameStartChecker;

  private bool _isMaxPlayers;
  private void OnEnable()
  {
    _gameStartChecker.GameStartEvent += OnGameStart;
  }

  private void Update()
  {
    _isMaxPlayers = _playerInputManager != null && _playerInputManager.maxPlayerCount <= _playerInputManager.playerCount;
    _joinTextGO.SetActive(!_isMaxPlayers);
  }

  private void OnDisable()
  {
    _gameStartChecker.GameStartEvent -= OnGameStart;
  }

  public void OnGameStart()
  {
    _countDownText.gameObject.SetActive(true);
    StartCoroutine(CountDownStartCounter(GameManager.instance.startGameContDownTime));
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
