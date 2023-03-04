using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

namespace AmericanFootballManager {
  public class Cap : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler {
    private RectTransform rectTransform;
    [Inject] private FormationWindow FormationWindow;
    [Inject(Id = "Players")] private GameObject[] Players;
    public GameObject MyPlayer;
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
    public void OnPointerClick(PointerEventData eventData) {
      FormationWindow.HandleSelect(this);
    }

    public void Select() {
      GetComponent<Image>().color = Color.red;
    }

    public void Unselect() {
      GetComponent<Image>().color = new Color(0.07f, 0f, 1f);
    }
  }
}