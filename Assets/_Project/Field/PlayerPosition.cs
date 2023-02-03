using UnityEngine;

namespace AmericanFootballManager {
  public enum Position { QB, C, others };
  public class PlayerPosition : MonoBehaviour {
    public Position Position = Position.others;
  }
}
