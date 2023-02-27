using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class FormationWindow : MonoBehaviour {
    [Inject] private ActionController ActionController;
    private GameObject[] Caps;
    [Inject(Id = "Players")] private GameObject[] Players;
    public void Start() {
      Apply();
      gameObject.SetActive(false);
    }

    public void Apply() {
      Caps = GameObject.FindGameObjectsWithTag("Cap");
      GameObject[] MyPlayers = new GameObject[11];
      int index = 0;
      foreach (var Player in Players) {
        if (!Player.GetComponent<PlayerMovement>().RightSide) {
          MyPlayers[index] = Player;
          index++;
        }
      }

      for (int i = 0; i < 11; i++) {
        Vector3 CapPosition = Caps[i].transform.localPosition;
        Vector3 PlayerPosition = Converter.CapToPlayerPosition(CapPosition);

        MyPlayers[i].transform.localPosition = PlayerPosition;
      }
    }
  }
}
