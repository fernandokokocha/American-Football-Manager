using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  class PlayDescription {
    public int Down;
    public float MarkerCurrent;
    public float MarkerToGo;

    public int ToGo() {
      return (int)(MarkerToGo - MarkerCurrent);
    }
  }

  class ActionController : MonoBehaviour {
    public PlayDescription CurrentAction;
    [Inject(Id = "Marker1")] public Marker Marker1;
    [Inject(Id = "Marker2")] public Marker Marker2;
    [Inject] Ball Ball;
    void Start() {
      CurrentAction = new PlayDescription {
        Down = 1,
        MarkerCurrent = 50.0f,
        MarkerToGo = 60.0f
      };
      UpdateMarkers();

      Ball.OnTackle += NextAction;
    }

    void NextAction() {
      PlayDescription NextAction = new PlayDescription {
        Down = CurrentAction.Down + 1,
        MarkerCurrent = Converter.XPositionToYards(Ball.transform.position.x),
        MarkerToGo = 60.0f
      };
      CurrentAction = NextAction;
      UpdateMarkers();
    }

    void UpdateMarkers() {
      Marker1.ChangeYards(CurrentAction.MarkerCurrent);
      Marker2.ChangeYards(CurrentAction.MarkerToGo);
    }
  }
}
