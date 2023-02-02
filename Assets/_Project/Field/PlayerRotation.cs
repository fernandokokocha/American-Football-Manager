using UnityEngine;
using System;

namespace AmericanFootballManager {
  public class PlayerRotation : MonoBehaviour {
    private Nullable<Vector3> RotateTowards;
    public float turnRate = 100;
    void Awake() {
      RotateTowards = null;
    }
    void Update() {
      if (RotateTowards.HasValue) {
        Quaternion toRotation = Quaternion.LookRotation(RotateTowards.Value, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnRate * Time.deltaTime);
      }
    }
    public void TurnImmediately(Vector3 direction) {
      transform.forward = direction;
    }
    public void TurnOverTime(Vector3 direction) {
      RotateTowards = direction;
    }
    public void StopRotating() {
      RotateTowards = null;
    }
  }
}
