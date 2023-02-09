using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class ProgramIdle : MonoBehaviour, IProgram {
    [Inject] private Ball Ball;
    public ProgramState ProgramState;
    public void Start() {
      StartCoroutine(StateMachine());
    }
    public IEnumerator StateMachine() {
      ProgramState = ProgramState.Idle;
      yield return new WaitUntil(Behaviour().SnapDone);
      ProgramState = ProgramState.Idle;
    }
    public ProgramState State() {
      return ProgramState;
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
  }
}
