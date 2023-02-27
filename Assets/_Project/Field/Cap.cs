using UnityEngine;
using UnityEngine.EventSystems;

public class Cap : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
  private RectTransform rectTransform;
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

  }
}
