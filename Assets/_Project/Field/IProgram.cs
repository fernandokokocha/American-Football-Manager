using System.Collections;

namespace AmericanFootballManager {
  public interface IProgram {
    public ProgramState State();
    public IEnumerator StateMachine();
  }
}
