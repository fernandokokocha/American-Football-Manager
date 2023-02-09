using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public enum AvailableProgram { QB, Snap, Idle };
  public class PlayerBehaviour : MonoBehaviour {
    public PlayerMovement PlayerMovement;
    public AvailableProgram ChosenProgram = AvailableProgram.Idle;
    private IProgram Program;
    private Rigidbody rb;
    [Inject] private Interface Interface;
    [Inject] private Ball Ball;
    [Inject] private DiContainer Container;
    private bool stop = false;
    public bool snap = false;
    void Start() {
      if (ChosenProgram == AvailableProgram.QB) {
        Program = Container.InstantiateComponent<ProgramQB>(gameObject);
      } else if (ChosenProgram == AvailableProgram.Snap) {
        Program = Container.InstantiateComponent<ProgramSnap>(gameObject);
      } else {
        Program = Container.InstantiateComponent<ProgramIdle>(gameObject);
      }

      rb = GetComponent<Rigidbody>();
      Interface.OnSnap += DoSnap;
      Ball.OnPassCompleted += RealizeProgram;
      Ball.OnTackle += HandleTackle;
    }
    void DoSnap() {
      snap = true;
      RealizeProgram();
    }
    public bool SnapDone() {
      return snap;
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
        rb.isKinematic = true;
        return;
      }

      ProgramState myState = Program.State();
      if (myState == ProgramState.Idle) {
        PlayerMovement.Idle();
      } else if (myState == ProgramState.WalkBack) {
        PlayerMovement.WalkBack();
      } else if (myState == ProgramState.RunForward) {
        PlayerMovement.WalkForward();
      }
    }
    public bool HasBall() {
      Ball[] balls = GetComponentsInChildren<Ball>();
      return (balls.Length > 0);
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
