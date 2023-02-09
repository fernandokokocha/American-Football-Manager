using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public enum AvailableProgram { QB, Snap, RunForward, RunToBall, Idle };
  public class PlayerBehaviour : MonoBehaviour {
    public PlayerMovement PlayerMovement;
    public AvailableProgram ChosenProgram = AvailableProgram.Idle;
    private IProgram Program;
    private Rigidbody rb;
    [Inject] private Interface Interface;
    [Inject] private Ball Ball;
    [Inject] private DiContainer Container;
    [Inject] public Team Team;
    private bool stop = false;
    public bool snap = false;
    public bool wait = false;
    void Start() {
      if (ChosenProgram == AvailableProgram.QB) {
        Program = Container.InstantiateComponent<ProgramQB>(gameObject);
      } else if (ChosenProgram == AvailableProgram.Snap) {
        Program = Container.InstantiateComponent<ProgramSnap>(gameObject);
      } else if (ChosenProgram == AvailableProgram.RunForward) {
        Program = Container.InstantiateComponent<ProgramRunForward>(gameObject);
      } else if (ChosenProgram == AvailableProgram.RunToBall) {
        Program = Container.InstantiateComponent<ProgramRunToBall>(gameObject);
      } else {
        Program = Container.InstantiateComponent<ProgramIdle>(gameObject);
      }

      rb = GetComponent<Rigidbody>();
      Interface.OnSnap += DoSnap;
      Ball.OnTackle += HandleTackle;
    }
    void DoSnap() {
      snap = true;
    }
    void HandleTackle() {
      stop = true;
    }
    void OnCollisionEnter(Collision collision) {
      if (!collision.gameObject.CompareTag("Player")) return;

      PlayerBehaviour otherPlayer = collision.gameObject.GetComponent<PlayerBehaviour>();
      if (Team == otherPlayer.Team) {
        Debug.Log("Kolizja z teammatem");
        return;
      }

      if (HasBall()) {
        PlayerMovement.GetTackled();
        Ball.GetTackled();
        return;
      }
      StartCoroutine(WaitAndRealize());
    }
    public void Update() {
      if (!snap) {
        return;
      }

      if (stop) {
        rb.isKinematic = true;
        PlayerMovement.Idle();
        return;
      }

      if (wait) {
        rb.isKinematic = true;
        PlayerMovement.Idle();
        return;
      }

      rb.isKinematic = false;
      ProgramState myState = Program.State();
      if (myState == ProgramState.Idle) {
        PlayerMovement.Idle();
      } else if (myState == ProgramState.WalkBack) {
        PlayerMovement.WalkBack();
      } else if (myState == ProgramState.RunForward) {
        PlayerMovement.WalkForward();
      } else if (myState == ProgramState.RunToBall) {
        PlayerMovement.TurnAndWalk(GetToBallDirection());
      }
    }
    public bool HasBall() {
      Ball[] balls = GetComponentsInChildren<Ball>();
      return (balls.Length > 0);
    }
    IEnumerator WaitAndRealize() {
      wait = true;
      yield return new WaitForSeconds(1);
      wait = false;
    }
    Vector3 GetToBallDirection() {
      Vector3 ballPosition = Ball.transform.position;
      Vector3 directionToBall = ballPosition - transform.position;
      directionToBall.Normalize();
      return directionToBall;
    }
  }
}
