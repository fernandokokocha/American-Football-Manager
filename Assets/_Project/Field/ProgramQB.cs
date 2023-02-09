using System.Collections;
using UnityEngine;
using Zenject;
using System;

namespace AmericanFootballManager {
  public enum State { Idle, WalkBack, RunForward };
  public class ProgramQB : MonoBehaviour {
    [Inject] private Ball Ball;
    public State State;
    private bool go = false;
    public void Start() {
      StartCoroutine(StateMachine());
    }
    IEnumerator StateMachine() {
      State = State.Idle;
      yield return new WaitUntil(Behaviour().SnapDone);
      State = State.WalkBack;
      Behaviour().RealizeProgram();
      yield return new WaitForSeconds(2);
      State = State.RunForward;
      Behaviour().RealizeProgram();
    }
    private PlayerBehaviour Behaviour() {
      return GetComponent<PlayerBehaviour>();
    }
  }
}
