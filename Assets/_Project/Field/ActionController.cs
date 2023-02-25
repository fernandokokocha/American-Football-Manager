using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  class PlayDescription {
    public int Down;
    public float MarkerCurrent;
    public float MarkerToGo;
  }

  class ActionController : MonoBehaviour {
    public PlayDescription CurrentAction;
    [Inject(Id = "Marker1")] public Marker Marker1;
    [Inject(Id = "Marker2")] public Marker Marker2;
    void Start() {
      CurrentAction = new PlayDescription {
        Down = 1,
        MarkerCurrent = 50.0f,
        MarkerToGo = 60.0f
      };

      Marker1.ChangeYards(CurrentAction.MarkerCurrent);
      Marker2.ChangeYards(CurrentAction.MarkerToGo);
    }
  }
}
