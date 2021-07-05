using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ArenaConfiguration : MonoBehaviour, IArenaLoadTrigger
{
  [SerializeField] private bool _canFight = true;
  [SerializeField] private int _timeToStart = 3;
  [SerializeField] private List<Transform> _spawnPointsList = new List<Transform>();

  private List<PlayerHealth> _playersList = new List<PlayerHealth>();
  private List<PlayerHealth> _defeatedPlayersList = new List<PlayerHealth>();

  public event UnityAction<int> ArenaLoadEvent;
  public event UnityAction ArenaLoadCanceledEvent;

  private void Awake()
  {
    GameManager.instance.CanFight = _canFight;

    PlayerInputManager.instance.DisableJoining();

    _playersList = FindObjectsOfType<PlayerHealth>().ToList();

    foreach(PlayerHealth player in _playersList)
    {
      player.DeathEvent += OnPlayerDeath;
    }

    SpawnPlayersOnRandomPoints();
  }

  private void OnPlayerDeath(PlayerHealth player)
  {
    player.DeathEvent -= OnPlayerDeath;
    _playersList.Remove(player);

    if(_playersList.Count == 1)
    {
      ApplicationVariables.LastRoundWinnerName = _playersList[0].PlayerNumber;
      _defeatedPlayersList = FindObjectsOfType<PlayerHealth>(true).ToList();
      foreach (PlayerHealth defeatedPlayer in _defeatedPlayersList)
      {
        GameManager.instance.CanFight = false;
        defeatedPlayer.ResetHealth();
      }
      ArenaLoadEvent?.Invoke(_timeToStart);
    }
  }

  private void SpawnPlayersOnRandomPoints()
  {
    List<Transform> playersTransformList = _playersList.Select(p => p.gameObject.transform).ToList();
    bool isEnoughtSpawnPoints = _spawnPointsList.Count >= _playersList.Count;

    foreach (Transform player in playersTransformList)
    {
      int randomSpawnPointsIndex = Random.Range(0, _spawnPointsList.Count);
      player.position = _spawnPointsList[randomSpawnPointsIndex].position;

      if (isEnoughtSpawnPoints) { continue; }
      _spawnPointsList.RemoveAt(randomSpawnPointsIndex);
    }
  }
}
