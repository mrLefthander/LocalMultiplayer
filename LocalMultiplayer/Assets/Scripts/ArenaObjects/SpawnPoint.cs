using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
  public enum SpawnPointStatus
  {
    Empty,
    Occupied
  }

  public SpawnPointStatus Status;
}
