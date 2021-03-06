using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ArenaConfiguration : MonoBehaviour, IArenaLoadTrigger
{
  [SerializeField] private int _timeToStartNextLevel = 3;  
  
  private List<Transform> _spawnPointsList = new List<Transform>();
  private List<PlayerHealth> _playersList = new List<PlayerHealth>();

  public event UnityAction<int> ArenaLoadEvent;
  public event UnityAction ArenaLoadCanceledEvent;

  private void Awake()
  {
    _playersList = FindObjectsOfType<PlayerHealth>().ToList();

    GetArenaSpawnPoints();
    SubscribeOnPlayersDeath();

    GameManager.instance.StartRound();
    SpawnPlayersOnRandomPoints();
  }

  private void GetArenaSpawnPoints()
  {
    if (_spawnPointsList.Count != 0) { return; }

    _spawnPointsList = GetComponentsInChildren<SpawnPoint>().Select(p => p.transform).ToList();
  }

  private void SubscribeOnPlayersDeath()
  {
    foreach (PlayerHealth player in _playersList)
    {
      player.DeathEvent += OnPlayerDeath;
    }
  }

  private void OnPlayerDeath(PlayerHealth player)
  {
    player.DeathEvent -= OnPlayerDeath;
    _playersList.Remove(player);

    if (_playersList.Count != 1) { return; }

    EndArena();
  }

  private void EndArena()
  {
    GameManager.instance.EndRound(_playersList[0]);
    _playersList = FindObjectsOfType<PlayerHealth>(true).ToList();

    if (GameManager.instance.GameWin) 
    {
      DestroyAllPlayers();
      return; 
    }

    RespawnAllPlayers();
    ArenaLoadEvent?.Invoke(_timeToStartNextLevel);
  }

  public void DestroyAllPlayers()
  {
    if(_playersList.Count == 0 || _playersList == null) { return; }

    foreach (PlayerHealth player in _playersList)
    {
      player.gameObject.SetActive(false);
      Destroy(player.gameObject);
    }
  }

  private void RespawnAllPlayers()
  {
    foreach (PlayerHealth player in _playersList)
    {
      player.ResetHealth();
    }
  }

  private void SpawnPlayersOnRandomPoints()
  {
    List<Transform> playersTransformList = _playersList.Select(p => p.transform).ToList();
    bool isEnoughtSpawnPoints = _spawnPointsList.Count >= _playersList.Count;

    foreach (Transform player in playersTransformList)
    {
      int randomSpawnPointsIndex = Random.Range(0, _spawnPointsList.Count);
      player.position = _spawnPointsList[randomSpawnPointsIndex].transform.position;

      if (!isEnoughtSpawnPoints) { continue; }
      _spawnPointsList.RemoveAt(randomSpawnPointsIndex);
    }
  }
}
