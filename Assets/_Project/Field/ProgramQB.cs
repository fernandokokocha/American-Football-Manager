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
      ProgramState = ProgramState.WalkBack;
      yield return new WaitForSeconds(2);
      ProgramState = ProgramState.RunForward;
    }
    public ProgramState State() {
      return ProgramState;
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
  }
}
