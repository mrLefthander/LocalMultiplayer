using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionDetection : MonoBehaviour
{
  private const float BOX_CAST_ANGLE = 0f;

  public bool IsGrounded { get; private set; }

  private Collider2D _collider;

  private void Awake()
  {
    _collider = GetComponent<Collider2D>();
  }

  private void FixedUpdate()
  {
    IsGrounded = UpdateIsGrounded();
  }

  private bool UpdateIsGrounded()
  {
    RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.extents, BOX_CAST_ANGLE,
      Vector2.down, _collider.bounds.size.y, ApplicationVariables.LayerNames.GroundLayerMask);

    return raycastHit.collider != null;
  }
}
