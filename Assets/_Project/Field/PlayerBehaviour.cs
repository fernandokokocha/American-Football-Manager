using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AmericanFootballManager {
    public enum Program { holdPosition, runForward };
    public class PlayerBehaviour : MonoBehaviour {
        public PlayerMovement PlayerMovement;
        public Program Program;
        void Start() {
            RealizeProgram();
        }
        void OnCollisionEnter(Collision collision) {
            if (!collision.gameObject.CompareTag("Player")) return;
            StartCoroutine(WaitAndRealize());
        }
        void RealizeProgram() {
            if (Program == Program.holdPosition) {
                PlayerMovement.Idle();
            } else if (Program == Program.runForward) {
                PlayerMovement.WalkForward();
            }
        }
        IEnumerator WaitAndRealize() {
            PlayerMovement.Idle();
            yield return new WaitForSeconds(2);
            RealizeProgram();
        }
    }
}
