using UnityEngine;

namespace AmericanFootballManager {
  public class Marker : MonoBehaviour {
    public float yards;
    public float height = 15.0f;
    public void ChangeYards(float newYards) {
      yards = newYards;
      float x = Converter.YardsToXPosition(yards);
      transform.localPosition = new Vector3(x, height, 275f);
    }
  }
}
