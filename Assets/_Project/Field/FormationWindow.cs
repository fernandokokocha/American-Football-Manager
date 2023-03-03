using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class FormationWindow : MonoBehaviour {
    [Inject] private ActionController ActionController;
    private Cap Selected;
    public void Start() {
      Apply();
      gameObject.SetActive(false);
    }

    public void Apply() {
      GameObject[] Caps = GameObject.FindGameObjectsWithTag("Cap");

      for (int i = 0; i < 11; i++) {
        Cap Cap = Caps[i].GetComponent<Cap>();
        Cap.RepositionPlayer();
      }
    }

    public void HandleSelect(Cap Cap) {
      if (Selected) Selected.Unselect();
      Selected = Cap;
      Cap.Select();
    }
  }
}
