using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameStartChecker : MonoBehaviour
{
  [SerializeField] private LayerMask _playerLayerMask;
  [SerializeField] private PlayerInputManager _playerInputManager;

  public event UnityAction GameStartEvent = delegate { };

  private int _playersInStartZoneNumber;

  private void Update()
  {
    if (_playersInStartZoneNumber >= 2 && _playersInStartZoneNumber == _playerInputManager.playerCount)
    {
      _playersInStartZoneNumber = 0;
      GameStartEvent?.Invoke();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (IsTriggeredByPlayer(other))
      _playersInStartZoneNumber++;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (IsTriggeredByPlayer(other))
      _playersInStartZoneNumber--;
  }

  private bool IsTriggeredByPlayer(Collider2D other)
  {
    return _playerLayerMask == (_playerLayerMask | (1 << other.gameObject.layer));
  }
}
