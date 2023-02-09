using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class ProgramRunToBall : MonoBehaviour, IProgram {
    [Inject] private Ball Ball;
    [Inject(Id = "QB")] private PlayerPosition MyQB;
    public ProgramState ProgramState;
    public void Start() {
      StartCoroutine(StateMachine());
    }
    public IEnumerator StateMachine() {
      ProgramState = ProgramState.RunToBall;
      yield return null;
    }
    public ProgramState State() {
      return ProgramState;
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
  }
}
