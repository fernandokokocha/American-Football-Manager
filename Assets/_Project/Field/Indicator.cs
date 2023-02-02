using UnityEngine;

namespace AmericanFootballManager {
  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class Indicator : MonoBehaviour {
    public PlayerAppearence PlayerToShow;
    void Update() {
      Vector3 playerPosition = PlayerToShow.transform.position;

      var mesh = new Mesh { name = "Player Indicator" };
      GetComponent<MeshFilter>().mesh = mesh;

      mesh.vertices = new Vector3[] {
        new Vector3(playerPosition.x + 12, 25, playerPosition.z),
        new Vector3(playerPosition.x - 8, 50, playerPosition.z),
        new Vector3(playerPosition.x + 32, 50, playerPosition.z)
      };

      mesh.triangles = new int[] { 0, 1, 2 };

      mesh.normals = new Vector3[] {
        Vector3.back, Vector3.back, Vector3.back
      };
    }
  }
}
