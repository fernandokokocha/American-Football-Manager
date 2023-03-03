using UnityEngine;

namespace AmericanFootballManager {
  public enum Position { QB, C, Line, others };
  public class PlayerPosition : MonoBehaviour {
    public Position Position = Position.others;
  }
}
