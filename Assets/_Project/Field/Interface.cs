using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
  public class Interface : MonoBehaviour {
    public static event Action OnSnap;
    public bool snapped;
    private int boxHeight = 50;
    private int textHeight = 30;
    private ActionController ActionController;
    [Inject] private FormationWindow FormationWindow;
    private bool formationShown;
    private bool tackled;
    void Start() {
      formationShown = false;
      snapped = false;
      tackled = false;
      OnSnap += HandleSnap;
      Ball.OnTackle += HandleTackle;
      ActionController = GameObject.Find("ActionController").GetComponent<ActionController>();
    }
    public void OnDestroy() {
      Interface.OnSnap -= HandleSnap;
      Ball.OnTackle -= HandleTackle;
    }
    void OnGUI() {
      GUIStyle style = new();
      style.normal.textColor = Color.black;
      style.fontSize = 26;

      GUI.Box(new Rect(0, 0, Screen.width, boxHeight), "");

      if (!formationShown && !snapped) {
        if (GUI.Button(new Rect(10, 10, 50, textHeight), "Snap"))
          OnSnap?.Invoke();
      }

      if (tackled) {
        if (GUI.Button(new Rect(10, 10, 90, textHeight), "Next action"))
          ActionController.GoToNextScene();
      }

      String[] downDescs = { "", "1st", "2nd", "3rd", "4th" };
      String downDesc = downDescs[ActionController.CurrentAction.Down];
      GUI.Label(new Rect(120, 10, 100, textHeight), $"{downDesc} down", style);

      int toGo = ActionController.CurrentAction.ToGo();
      GUI.Label(new Rect(240, 10, 100, textHeight), $"{toGo} yd to go", style);

      if (!snapped) {
        String FormationLabel = formationShown ? "Close" : "Formation";
        if (GUI.Button(new Rect(Screen.width - 110, 10, 100, textHeight), FormationLabel)) {
          FormationWindow.gameObject.SetActive(!FormationWindow.gameObject.activeInHierarchy);
          formationShown = !formationShown;
        }
      }
    }
    void HandleSnap() {
      snapped = true;
    }
    void HandleTackle() {
      tackled = true;
    }
    public bool HasSnapped() {
      return snapped;
    }
  }
}