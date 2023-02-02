using UnityEngine;

namespace AmericanFootballManager {
  public enum Position { QB, others };
  public class PlayerPosition : MonoBehaviour {
    public Position Position = Position.others;
  }
}
