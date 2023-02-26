using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public enum AvailableProgram { QB, Snap, RunForward, RunToBall, Cover, Idle };
  public class PlayerBehaviour : MonoBehaviour {
    public PlayerMovement PlayerMovement;
    public AvailableProgram ChosenProgram = AvailableProgram.Idle;
    [Range(1, 11)]
    public int ChosenPlayer;
    private IProgram Program;
    private Rigidbody rb;
    [Inject] private Interface Interface;
    [Inject] private Ball Ball;
    [Inject] private DiContainer Container;
    [Inject] public Team Team;
    [Inject(Id = "Players")] private GameObject[] AllPlayers;
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
      } else if (ChosenProgram == AvailableProgram.Cover) {
        Program = Container.InstantiateComponent<ProgramCover>(gameObject);
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
        rb.isKinematic = true;
        PlayerMovement.Idle();
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
        PlayerMovement.TurnAndWalk(GetToDirection(Ball.transform.position));
      } else if (myState == ProgramState.Cover) {
        PlayerMovement.TurnAndWalk(GetToDirection(MyCover().transform.position));
      }
    }
    private GameObject MyCover() {
      int who = 0;
      foreach (GameObject player in AllPlayers) {
        if (player.GetComponent<PlayerBehaviour>().Team != Team) {
          who++;
          if (who == ChosenPlayer) {
            return player;
          }
        }
      }
      return null;
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
    Vector3 GetToDirection(Vector3 position) {
      Vector3 directionToBall = position - transform.position;
      directionToBall.Normalize();
      return directionToBall;
    }
  }
}
