using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace AmericanFootballManager {

  public class Cap : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    private RectTransform rectTransform;
    [Inject] private FormationWindow FormationWindow;

    private void Awake() {
      rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData) {

    }
    public void OnBeginDrag(PointerEventData eventData) {

    }
    public void OnDrag(PointerEventData eventData) {
      rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
      FormationWindow.Apply();
    }
  }
}