using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class FormationWindow : MonoBehaviour {
    [Inject] private ActionController ActionController;
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
  }
}
