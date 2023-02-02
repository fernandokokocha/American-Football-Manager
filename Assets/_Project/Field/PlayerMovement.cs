using UnityEngine;

namespace AmericanFootballManager {
  public class PlayerMovement : MonoBehaviour {
    public bool RightSide;
    private Vector3 FacingDirection;
    private Vector3 BackDirection;
    private Rigidbody rb;
    public float speed;
    public PlayerAnimation PlayerAnimation;
    public PlayerRotation PlayerRotation;
    void Awake() {
      rb = GetComponent<Rigidbody>();

      if (RightSide) FacingDirection = Vector3.left;
      else FacingDirection = Vector3.right;
      transform.forward = FacingDirection;

      BackDirection = FacingDirection * -1;
    }
    public void TurnAndWalkForward() {
      PlayerRotation.TurnOverTime(FacingDirection);
      rb.velocity = FacingDirection * speed;
      PlayerAnimation.MoveForward();
    }
    public void TurnAndWalkBack() {
      PlayerRotation.TurnOverTime(BackDirection);
      rb.velocity = BackDirection * speed;
      PlayerAnimation.MoveForward();
    }
    public void WalkBack() {
      rb.velocity = BackDirection * speed / 3.0f;
      PlayerAnimation.MoveBackward();
    }
    public void TurnAndWalk(Vector3 direction) {
      PlayerRotation.TurnOverTime(direction);
      rb.velocity = direction * speed;
      PlayerAnimation.MoveForward();
    }
    public void WalkForward() {
      rb.velocity = FacingDirection * speed;
      PlayerAnimation.MoveForward();
    }
    public void Idle() {
      PlayerRotation.StopRotating();
      rb.velocity = Vector3.zero;
      PlayerAnimation.Idle();
    }
  }
}
