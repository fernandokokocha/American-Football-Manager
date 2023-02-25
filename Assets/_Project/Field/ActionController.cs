using UnityEngine;

namespace AmericanFootballManager {
  class PlayDescription {
    public int Down;
    public float MarkerCurrent;
    public float MarkerToGo;
  }

  class ActionController : MonoBehaviour {
    public PlayDescription CurrentAction;

    void Start() {
      CurrentAction = new PlayDescription {
        Down = 1,
        MarkerCurrent = 50.0f,
        MarkerToGo = 60.0f
      };
    }
  }
}
