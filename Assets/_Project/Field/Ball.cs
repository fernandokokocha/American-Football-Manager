using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
  public class Ball : MonoBehaviour {
    float speed = 100.0f;
    private Nullable<Vector3> TargetPosition = null;
    private PlayerPosition Target = null;
    [Inject] Indicator Indicator;

    public void Update() {
      if (TargetPosition.HasValue) {
        float distance = Vector3.Distance(transform.position, TargetPosition.Value);
        if (distance < 1.0f) {
          TransferBall();
        } else {
          ChangePosition();
        }
      }
    }

    public void ThrowTo(PlayerPosition Player) {
      transform.SetParent(null);
      Indicator.PlayerToShow = null;

      Vector3 position = Player.GetComponent<Transform>().position;
      TargetPosition = position;
      Target = Player;
    }

    private void ChangePosition() {
      transform.position = Vector3.MoveTowards(
          transform.position,
          TargetPosition.Value,
          speed * Time.deltaTime
      );
    }

    private void TransferBall() {
      transform.SetParent(Target.transform);
      Indicator.PlayerToShow = Target.GetComponent<PlayerAppearence>();

      TargetPosition = null;
      Target = null;
    }
  }
}
