using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayerKeyboard2 : MonoBehaviour
{
  [SerializeField]
  private GameObject _playerToLoad;

  private bool _hasPlayerLoaded = false;

  private void Update()
  {
    if (!_hasPlayerLoaded)
    {
      if(Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame ||
        Keyboard.current.iKey.wasPressedThisFrame || Keyboard.current.kKey.wasPressedThisFrame)
      {
        Instantiate(_playerToLoad, transform.position, transform.rotation);
        _hasPlayerLoaded = true;
      }
    }
  }
}
