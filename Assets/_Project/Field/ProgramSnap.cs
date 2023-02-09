using System.Collections;
using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class ProgramSnap : MonoBehaviour, IProgram {
    [Inject] private Ball Ball;
    [Inject(Id = "QB")] private PlayerPosition MyQB;
    [Inject] private Interface Interface;
    public ProgramState ProgramState;
    public void Start() {
      StartCoroutine(StateMachine());
    }
    public IEnumerator StateMachine() {
      yield return new WaitUntil(Interface.HasSnapped);
      ThrowBallTo(MyQB);
      ProgramState = ProgramState.Idle;
    }
    public ProgramState State() {
      return ProgramState;
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
    void ThrowBallTo(PlayerPosition MyQB) {
      Ball.ThrowTo(MyQB);
    }
  }
}
