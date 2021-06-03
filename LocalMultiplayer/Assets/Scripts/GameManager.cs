using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
  private const int DEFAULT_MAX_PLAYERS_COUNT = 4;

  public static GameManager instance;

  [SerializeField]
  private PlayerInputManager _playerInputManager;
  [SerializeField]
  private GameObject _playerSpawnEffect;

  private int _maxPlayers;

  private List<PlayerMovement> _activePlayers = new List<PlayerMovement>();

  private void Awake()
  {
    SetUpSingleton();
  }

  private void Start()
  {
    if(_playerInputManager == null)
      _playerInputManager = FindObjectOfType<PlayerInputManager>();

    if (_playerInputManager != null)
      _maxPlayers = _playerInputManager.maxPlayerCount;
    else
      _maxPlayers = DEFAULT_MAX_PLAYERS_COUNT;
  }

  public void AddPlayer(PlayerMovement newPlayer)
  {
    if(_activePlayers.Count < _maxPlayers)
    {
      _activePlayers.Add(newPlayer);
      Instantiate(_playerSpawnEffect, newPlayer.transform.position, Quaternion.identity);
    }
    else
    {
      Destroy(newPlayer.gameObject);
    }
  }

  public bool IsMaxPlayers()
  {
    return _activePlayers.Count == _maxPlayers;
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
