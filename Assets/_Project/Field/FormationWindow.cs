using UnityEngine;
using Zenject;

namespace AmericanFootballManager {
  public class FormationWindow : MonoBehaviour {
    [Inject] private ActionController ActionController;
    public void Start() {
      gameObject.SetActive(false);
    }
  }
}
