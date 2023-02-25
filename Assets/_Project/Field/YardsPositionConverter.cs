namespace AmericanFootballManager {
  public class YardsPositionConverter {
    public static float YardsToXPosition(float yards) {
      return yards * 10f - 500f;
    }

    public static float XPositionToYards(float x) {
      return (x + 500f) / 10f;
    }
  }
}
