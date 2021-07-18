using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
  [SerializeField] private List<Transform> _exitPoints;
  [SerializeField] private GameObject _portalExitEffect;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!ApplicationVariables.LayerNames.IsTouchingPlayer(other.gameObject.layer)) { return; }

    AudioManager.instance.PlaySound(Sound.Type.Portal);
    Vector3 randomExitPosition = _exitPoints[Random.Range(0, _exitPoints.Count)].position;
    other.transform.position = randomExitPosition;
    Instantiate(_portalExitEffect, randomExitPosition, Quaternion.identity);
  }
}
