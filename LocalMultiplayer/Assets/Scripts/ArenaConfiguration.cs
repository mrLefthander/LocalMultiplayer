using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArenaConfiguration : MonoBehaviour
{
  [SerializeField] private bool _canFight = true;
  [SerializeField] private List<Transform> _spawnPointsList = new List<Transform>();

  private List<Transform> _playersList = new List<Transform>();

  private void Start()
  {
    GameManager.instance.CanFight = _canFight;
    PlayerInputManager.instance.DisableJoining();

    _playersList = FindObjectsOfType<PlayerHealth>().Select(p => p.gameObject.transform).ToList();

    SpawnPlayersOnRandomPoints();
  }

  private void SpawnPlayersOnRandomPoints()
  {
    bool isEnoughtSpawnPoints = _spawnPointsList.Count >= _playersList.Count;
    foreach (Transform player in _playersList)
    {
      int randomSpawnPointsIndex = Random.Range(0, _spawnPointsList.Count);
      player.position = _spawnPointsList[randomSpawnPointsIndex].position;

      if (isEnoughtSpawnPoints) { continue; }
      _spawnPointsList.RemoveAt(randomSpawnPointsIndex);
    }
  }
}
