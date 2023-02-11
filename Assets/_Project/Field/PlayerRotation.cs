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
        Vector3 lookTo = RotateTowards.Value;
        lookTo.y = 0;
        Quaternion toRotation = Quaternion.LookRotation(lookTo, Vector3.up);
        Quaternion rotateToBall = Quaternion.RotateTowards(transform.rotation, toRotation, turnRate * Time.deltaTime);
        transform.rotation = rotateToBall;
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
