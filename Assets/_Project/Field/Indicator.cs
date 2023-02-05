using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class Indicator : MonoBehaviour {
#nullable enable
    [Inject(Id = "C")] public PlayerAppearence? PlayerToShow;
#nullable disable
    void Update() {
      if (PlayerToShow == null) {
        GetComponent<MeshFilter>().mesh = null;
        return;
      }

      Vector3 playerPosition = PlayerToShow.transform.position;

      var mesh = new Mesh { name = "Player Indicator" };
      GetComponent<MeshFilter>().mesh = mesh;

      mesh.vertices = new Vector3[] {
        new Vector3(playerPosition.x + 12, 25, playerPosition.z + 50),
        new Vector3(playerPosition.x - 8, 50, playerPosition.z + 50),
        new Vector3(playerPosition.x + 32, 50, playerPosition.z + 50)
      };

      mesh.triangles = new int[] { 0, 1, 2 };

      mesh.normals = new Vector3[] {
        Vector3.back, Vector3.back, Vector3.back
      };
    }
  }
}
