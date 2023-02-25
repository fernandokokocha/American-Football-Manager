using UnityEngine;

namespace AmericanFootballManager {
  public class Marker : MonoBehaviour {
    public float yards;
    public float height = 15.0f;
    public void ChangeYards(float newYards) {
      yards = newYards;
      transform.position = new Vector3(YardsToXPosition(yards), height, 275f);
    }
    private float YardsToXPosition(float yards) {
      return yards * 10f - 500f;
    }
  }
}
