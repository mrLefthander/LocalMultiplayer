using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
  private const string POWER_UPS_PARENT_GO_NAME = "PowerUps";

  [SerializeField] private float _minTimeToSpawnPowerUp = 8f;
  [SerializeField] private float _maxTimeToSpawnPowerUp = 12f;
  [SerializeField] private int _maxActivePowerUpsCount = 5;
  [SerializeField] private List<PowerUp> _powerUpPrefabsList = new List<PowerUp>();

  private float _powerUpSpawnTimer;
  private Transform _powerUpsParent;
  private List<SpawnPoint> _spawnPointsList = new List<SpawnPoint>();
  private List<PowerUp> _activePowerUps = new List<PowerUp>();

  private void Awake()
  {
    GetArenaSpawnPoints();
    SetUpPowerUpsHierarchyParent();
    RandomizePowerUpSpawnTimer();
  }

  private void SetUpPowerUpsHierarchyParent()
  {
    if (_powerUpsParent) { return; }

    _powerUpsParent = new GameObject(POWER_UPS_PARENT_GO_NAME).transform;
    _powerUpsParent.parent = transform;
  }

  private void GetArenaSpawnPoints()
  {
    if (_spawnPointsList.Count != 0) { return; }

    _spawnPointsList = GetComponentsInChildren<SpawnPoint>().ToList();
  }

  private void Update()
  {
    if (_powerUpSpawnTimer > 0 || _activePowerUps.Count >= _maxActivePowerUpsCount)
    {
      _powerUpSpawnTimer -= Time.deltaTime;
      return;
    }

    SpawnPowerUpOnRandomPoint();
    RandomizePowerUpSpawnTimer();
  }

  private void SpawnPowerUpOnRandomPoint()
  {
    int randomSpawnPointIndex = GetRandomEmptySpawnPointIndex();
    int randomPowerUpIndex = Random.Range(0, _powerUpPrefabsList.Count);
    PowerUp powerUp = Instantiate(_powerUpPrefabsList[randomPowerUpIndex], _spawnPointsList[randomSpawnPointIndex].transform.position, Quaternion.identity, _powerUpsParent);
    powerUp.PickUpEvent += OnPowerUpPicked;
    powerUp.OccupiedSpawnPoint = _spawnPointsList[randomSpawnPointIndex];
    _spawnPointsList[randomSpawnPointIndex].Status = SpawnPoint.SpawnPointStatus.Occupied;
    _activePowerUps.Add(powerUp);
  }

  private void OnPowerUpPicked(PowerUp picked)
  {
    picked.PickUpEvent -= OnPowerUpPicked;
    picked.OccupiedSpawnPoint.Status = SpawnPoint.SpawnPointStatus.Empty;
    _activePowerUps.Remove(picked);
  }

  private int GetRandomEmptySpawnPointIndex()
  {
    int randomSpawnPointIndex = Random.Range(0, _spawnPointsList.Count);
    if(_spawnPointsList[randomSpawnPointIndex].Status == SpawnPoint.SpawnPointStatus.Occupied)
    {
      return GetRandomEmptySpawnPointIndex();
    }
    return randomSpawnPointIndex;
  }

  private void RandomizePowerUpSpawnTimer()
  {
    _powerUpSpawnTimer = Random.Range(_minTimeToSpawnPowerUp, _maxTimeToSpawnPowerUp);
  }
}
