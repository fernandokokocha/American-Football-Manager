using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class ProgramQB : MonoBehaviour, IProgram {
    [Inject] private Ball Ball;
    private ProgramState ProgramState;
    public void Start() {
      StartCoroutine(StateMachine());
    }
    public IEnumerator StateMachine() {
      ProgramState = ProgramState.Idle;
      yield return new WaitUntil(Behaviour().SnapDone);
      ProgramState = ProgramState.WalkBack;
      Behaviour().RealizeProgram();
      yield return new WaitForSeconds(2);
      ProgramState = ProgramState.RunForward;
      Behaviour().RealizeProgram();
    }
    public ProgramState State() {
      return ProgramState;
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
  }
}
