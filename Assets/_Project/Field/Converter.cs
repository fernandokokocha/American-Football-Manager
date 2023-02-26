using UnityEngine;

namespace AmericanFootballManager {
  public class Converter {
    public static float YardsToXPosition(float yards) {
      return yards * 10f - 500f;
    }

    public static float XPositionToYards(float x) {
      return (x + 500f) / 10f;
    }

    public static float CapXPositionToYards(float x) {
      return (x + 370f) / 7.4f;
    }
    public static float YardsToLocalXPosition(float yards) {
      return yards * 10f - 473f;
    }

    public static float CapYPositionToYards(float x) {
      return (x + 187f) / 4.6f;
    }
    public static float YardsToLocalZPosition(float yards) {
      return yards * 10f - 254f;
    }

    public static Vector3 CapToPlayerPosition(Vector3 capPosition) {
      float xYards = CapXPositionToYards(capPosition.x);
      float x = YardsToLocalXPosition(xYards);

      float y = -10f;

      float yYards = CapYPositionToYards(capPosition.y);
      float z = YardsToLocalZPosition(yYards);

      return new Vector3(x, y, z);
    }
  }
}
