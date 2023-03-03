using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace AmericanFootballManager {

  public class Cap : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    private RectTransform rectTransform;
    [Inject] private FormationWindow FormationWindow;
    [Inject(Id = "Players")] private GameObject[] Players;
    private GameObject MyPlayer;
    public int index;
    private void Awake() {
      rectTransform = GetComponent<RectTransform>();
      FindMyPlayer();
    }
    private void FindMyPlayer() {
      int tempIndex = index;
      foreach (var Player in Players) {
        if (!Player.GetComponent<PlayerMovement>().RightSide) {
          tempIndex--;
          if (tempIndex == 0) {
            MyPlayer = Player;
            break;
          }
        }
      }
    }
    public void RepositionPlayer() {
      Vector3 CapPosition = transform.localPosition;
      Vector3 PlayerPosition = Converter.CapToPlayerPosition(CapPosition);

      MyPlayer.transform.localPosition = PlayerPosition;
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