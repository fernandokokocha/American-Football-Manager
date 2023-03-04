using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace AmericanFootballManager {
  public class FormationField : MonoBehaviour, IPointerClickHandler {
    [Inject] private FormationWindow FormationWindow;
    public void OnPointerClick(PointerEventData eventData) {
      FormationWindow.HandleUnselect();
    }
  }
}