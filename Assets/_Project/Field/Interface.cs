using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
  public class Interface : MonoBehaviour {
    public static event Action OnSnap;
    public bool snapped;
    private int boxHeight = 50;
    private int textHeight = 30;
    [Inject] private ActionController ActionController;
    [Inject] private FormationWindow FormationWindow;
    void Start() {
      snapped = false;
      OnSnap += HandleSnap;
    }
    void OnGUI() {
      GUIStyle style = new();
      style.normal.textColor = Color.black;
      style.fontSize = 26;

      GUI.Box(new Rect(0, 0, Screen.width, boxHeight), "");

      if (GUI.Button(new Rect(10, 10, 50, textHeight), "Snap"))
        OnSnap?.Invoke();

      String[] downDescs = { "", "1st", "2nd", "3rd", "4th" };
      String downDesc = downDescs[ActionController.CurrentAction.Down];
      GUI.Label(new Rect(80, 10, 100, textHeight), $"{downDesc} down", style);

      int toGo = ActionController.CurrentAction.ToGo();
      GUI.Label(new Rect(200, 10, 100, textHeight), $"{toGo} yd to go", style);

      if (GUI.Button(new Rect(Screen.width - 110, 10, 100, textHeight), "Formation")) {
        FormationWindow.gameObject.SetActive(!FormationWindow.gameObject.activeInHierarchy);
      }
    }
    void HandleSnap() {
      snapped = true;
    }
    public bool HasSnapped() {
      return snapped;
    }
  }
}