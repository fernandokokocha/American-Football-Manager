using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace AmericanFootballManager {
    public enum Program { holdPosition, runForward, runToBall, snap, walkBack };
    public class PlayerBehaviour : MonoBehaviour {
        public PlayerMovement PlayerMovement;
        public Program Program;
        private Rigidbody rb;
        [Inject] private Interface Interface;
        [Inject] private Ball Ball;
        [Inject] private PlayerPosition MyQB;
        void Start() {
            rb = GetComponent<Rigidbody>();
            Interface.OnSnap += DoSnap;
        }
        void DoSnap() {
            RealizeProgram();
        }
        void OnCollisionEnter(Collision collision) {
            if (!collision.gameObject.CompareTag("Player")) return;
            rb.isKinematic = true;
            StartCoroutine(WaitAndRealize());
        }
        void RealizeProgram() {
            if (Program == Program.holdPosition) {
                PlayerMovement.Idle();
            } else if (Program == Program.runForward) {
                PlayerMovement.WalkForward();
            } else if (Program == Program.runToBall) {
                PlayerMovement.TurnAndWalk(GetToBallDirection());
            } else if (Program == Program.snap) {
                if (HasBall()) ThrowBallTo(MyQB);
                PlayerMovement.Idle();
            } else if (Program == Program.walkBack) {
                PlayerMovement.WalkBack();
            }
        }
        private bool HasBall() {
            Ball[] balls = GetComponentsInChildren<Ball>();
            return (balls.Length > 0);
        }
        void ThrowBallTo(PlayerPosition MyQB) {
            Ball.ThrowTo(MyQB);
        }
        Vector3 GetToBallDirection() {
            Vector3 ballPosition = new Vector3(-16.0200005f, 9.75f, -43.3199997f);
            Vector3 directionToBall = ballPosition - transform.position;
            directionToBall.Normalize();
            return directionToBall;
        }
        IEnumerator WaitAndRealize() {
            PlayerMovement.Idle();
            yield return new WaitForSeconds(2);
            rb.isKinematic = false;
            RealizeProgram();
        }
    }
}
