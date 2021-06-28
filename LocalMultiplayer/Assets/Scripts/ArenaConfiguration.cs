using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArenaConfiguration : MonoBehaviour
{
  [SerializeField] private bool _canFight = true;
  [SerializeField] private List<Transform> _spawnPointsList = new List<Transform>();

  private List<PlayerHealth> _playersList = new List<PlayerHealth>();
  private bool isRoundOver = false;

  private void Start()
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
      isRoundOver = true;
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
