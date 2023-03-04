using Zenject;
using UnityEngine;

namespace AmericanFootballManager {
  class AudioController : MonoBehaviour {
    [Inject] private Interface Interface;
    [Inject] private Ball Ball;
    public AudioSource Play;
    public AudioSource Referee;
    public AudioSource Crowd;
    public void Start() {
      Interface.OnSnap += HandleSnap;
      Ball.OnTackle += HandleTackle;
    }
    public void OnDestroy() {
      Interface.OnSnap -= HandleSnap;
      Ball.OnTackle -= HandleTackle;
    }
    private void HandleSnap() {
      Play.Play();
    }
    private void HandleTackle() {
      Play.Stop();
      Referee.Play();
    }
    public void StopAll() {
      Crowd.Stop();
      Play.Stop();
      Referee.Stop();
    }
  }
}
