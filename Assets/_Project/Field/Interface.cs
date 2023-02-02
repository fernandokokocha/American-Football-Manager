using UnityEngine;
using System;

namespace AmericanFootballManager {
  public class Interface : MonoBehaviour {
    public static event Action OnSnap;
    void OnGUI() {
      GUI.backgroundColor = Color.red;
      if (GUI.Button(new Rect(10, 10, 50, 30), "Snap"))
        OnSnap?.Invoke();
    }
  }
}