using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectUI : MonoBehaviour
{
  [SerializeField] private GameObject _joinText;
  [SerializeField] private PlayerInputManager _playerInputManager;

  private bool _isMaxPlayers;

  private void Update()
  {
    _isMaxPlayers = _playerInputManager != null && _playerInputManager.maxPlayerCount <= _playerInputManager.playerCount;
    _joinText.SetActive(!_isMaxPlayers);
  }
}
