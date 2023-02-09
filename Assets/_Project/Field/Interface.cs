using UnityEngine;
using System;

namespace AmericanFootballManager {
  public class Interface : MonoBehaviour {
    public static event Action OnSnap;
    public bool snapped;
    void Start() {
      snapped = false;
      OnSnap += HandleSnap;
    }
    void OnGUI() {
      GUI.backgroundColor = Color.red;
      if (GUI.Button(new Rect(10, 10, 50, 30), "Snap"))
        OnSnap?.Invoke();
    }
    void HandleSnap() {
      snapped = true;
    }
    public bool HasSnapped() {
      return snapped;
    }
  }
}