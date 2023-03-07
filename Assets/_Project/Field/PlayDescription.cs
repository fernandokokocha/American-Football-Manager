using UnityEngine;

namespace AmericanFootballManager {
  [CreateAssetMenu]
  public class PlayDescription : ScriptableObject {
    public int Down;
    public float MarkerCurrent;
    public float MarkerToGo;

    public int ToGo() {
      return (int)(MarkerToGo - MarkerCurrent);
    }
    public bool IsNullAction() {
      return (Down == 0);
    }
    public void SetFirstActionData() {
      Down = 1;
      MarkerCurrent = 50.0f;
      MarkerToGo = 60.0f;
    }
    public void CopyFrom(PlayDescription Other) {
      Down = Other.Down;
      MarkerCurrent = Other.MarkerCurrent;
      MarkerToGo = Other.MarkerToGo;
    }
  }
}
