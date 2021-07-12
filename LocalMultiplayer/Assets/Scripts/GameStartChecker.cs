using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameStartChecker : MonoBehaviour, IArenaLoadTrigger
{
  private const int MINIMUM_PLAYERS_COUNT_TO_START_GAME = 2;

  [SerializeField] private LayerMask _playerLayerMask;
  [SerializeField] private int _timeToStart = 3;

  public event UnityAction<int> ArenaLoadEvent = delegate { };
  public event UnityAction ArenaLoadCanceledEvent = delegate { };

  private int _playersInStartZoneNumber;
  private int _playersWasInStartZoneNumber;

  private void Update()
  {
    if (_playersInStartZoneNumber < MINIMUM_PLAYERS_COUNT_TO_START_GAME || !IsAllPlayersInStartZone()) { return; }

    _playersWasInStartZoneNumber = _playersInStartZoneNumber;
    _playersInStartZoneNumber = 0;

    ArenaLoadEvent?.Invoke(_timeToStart);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!IsTriggeredByPlayer(other)) { return; }
    _playersInStartZoneNumber++;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (!IsTriggeredByPlayer(other)) { return; }

    if (IsAllPlayersWasInStartZone())
    {
      _playersInStartZoneNumber = _playersWasInStartZoneNumber;
      _playersWasInStartZoneNumber = 0;
      ArenaLoadCanceledEvent?.Invoke();
    }

    _playersInStartZoneNumber--;
  }

  public bool IsMaxPlayers()
  {
    return PlayerInputManager.instance.maxPlayerCount <= PlayerInputManager.instance.playerCount;
  }

  private bool IsTriggeredByPlayer(Collider2D other)
  {
    return _playerLayerMask == (_playerLayerMask | (1 << other.gameObject.layer));
  }

  private bool IsAllPlayersInStartZone()
  {
    return IsEqualToMaxPlayersCount(_playersInStartZoneNumber);
  }

  private bool IsAllPlayersWasInStartZone()
  {
    return IsEqualToMaxPlayersCount(_playersWasInStartZoneNumber);
  }

  private bool IsEqualToMaxPlayersCount(int numberToCompare)
  {
    return numberToCompare == PlayerInputManager.instance.playerCount;
  }
}
