using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AmericanFootballManager {
    public enum Program { holdPosition, runForward, runToBall };
    public class PlayerBehaviour : MonoBehaviour {
        public PlayerMovement PlayerMovement;
        public Program Program;
        private Rigidbody rb;
        void Start() {
            rb = GetComponent<Rigidbody>();
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
                PlayerMovement.TurnAndWalk(GetBallDirection());
            }
        }
        Vector3 GetBallDirection() {
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
