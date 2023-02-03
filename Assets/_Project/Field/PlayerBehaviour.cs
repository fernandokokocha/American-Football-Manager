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
    private bool stop = false;
    void Start() {
      rb = GetComponent<Rigidbody>();
      Interface.OnSnap += DoSnap;
      Ball.OnPassCompleted += RealizeProgram;
      Ball.OnTackle += HandleTackle;
    }
    void DoSnap() {
      RealizeProgram();
    }
    void HandleTackle() {
      stop = true;
    }
    void OnCollisionEnter(Collision collision) {
      if (!collision.gameObject.CompareTag("Player")) return;
      if (HasBall()) {
        PlayerMovement.GetTackled();
        Ball.GetTackled();
        return;
      }
      rb.isKinematic = true;
      StartCoroutine(WaitAndRealize());
    }
    public void RealizeProgram() {
      if (stop) {
        PlayerMovement.Idle();
        return;
      }
      
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
        StartCoroutine(QBProgram());
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
    IEnumerator QBProgram() {
      PlayerMovement.WalkBack();
      yield return new WaitForSeconds(2);
      PlayerMovement.Idle();
    }
  }
}
