using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public enum Program { holdPosition, runForward, runToBall, snap, walkBack };
  public class PlayerBehaviour : MonoBehaviour {
    public PlayerMovement PlayerMovement;
    public Program Program;
    private Rigidbody rb;
    [Inject] private Interface Interface;
    [Inject] private Ball Ball;
    [Inject(Id = "QB")] private PlayerPosition MyQB;
    void Start() {
      rb = GetComponent<Rigidbody>();
      Interface.OnSnap += DoSnap;
      Ball.OnPassCompleted += RealizeProgram;
    }
    void DoSnap() {
      RealizeProgram();
    }
    void OnCollisionEnter(Collision collision) {
      if (!collision.gameObject.CompareTag("Player")) return;
      rb.isKinematic = true;
      StartCoroutine(WaitAndRealize());
    }
    public void RealizeProgram() {
      if (Program == Program.holdPosition) {
        PlayerMovement.Idle();
      } else if (Program == Program.runForward) {
        PlayerMovement.WalkForward();
      } else if (Program == Program.runToBall) {
        PlayerMovement.TurnAndWalk(GetToBallDirection());
      } else if (Program == Program.snap) {
        if (HasBall()) ThrowBallTo(MyQB);
        else PlayerMovement.Idle();
      } else if (Program == Program.walkBack) {
        PlayerMovement.WalkBack();
      }
    }
    private bool HasBall() {
      Ball[] balls = GetComponentsInChildren<Ball>();
      return (balls.Length > 0);
    }
    void ThrowBallTo(PlayerPosition MyQB) {
      Ball.ThrowTo(MyQB);
    }
    Vector3 GetToBallDirection() {
      Vector3 ballPosition = Ball.transform.position;
      Vector3 directionToBall = ballPosition - transform.position;
      directionToBall.Normalize();
      return directionToBall;
    }
    IEnumerator WaitAndRealize() {
      PlayerMovement.Idle();
      yield return new WaitForSeconds(2);
      rb.isKinematic = false;
      RealizeProgram();
    }
  }
}
