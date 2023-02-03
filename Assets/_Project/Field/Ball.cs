using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
  public class Ball : MonoBehaviour {
    float speed = 100.0f;
#nullable enable
    private PlayerPosition? Target = null;
#nullable disable
    [Inject] Indicator Indicator;
    public static event Action OnPassCompleted;
    public static event Action OnTackle;

    public void Update() {
      if (Target != null) {
        float distance = Vector3.Distance(transform.position, TargetPosition());
        if (distance < 1.0f) {
          TransferBall();
        } else {
          ChangePosition();
        }
      }
    }
    private Vector3 TargetPosition() {
      Vector3 targetPosition = Target.transform.position;
      targetPosition.y += 15;
      return targetPosition;
    }

    public void ThrowTo(PlayerPosition Player) {
      transform.SetParent(null);
      Indicator.PlayerToShow = null;

      Vector3 position = Player.GetComponent<Transform>().position;
      Target = Player;
    }

    private void ChangePosition() {
      transform.position = Vector3.MoveTowards(
          transform.position,
          TargetPosition(),
          speed * Time.deltaTime
      );
    }

    private void TransferBall() {
      transform.SetParent(Target.transform);
      OnPassCompleted?.Invoke();
      Indicator.PlayerToShow = Target.GetComponent<PlayerAppearence>();

      Target = null;
    }
    public void GetTackled() {
      GetComponent<CapsuleCollider>().isTrigger = false;
      OnTackle?.Invoke();
    }
  }
}
