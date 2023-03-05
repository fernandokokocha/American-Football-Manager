using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System.Collections;

namespace AmericanFootballManager {
  class PlayDescription {
    public int Down;
    public float MarkerCurrent;
    public float MarkerToGo;

    public int ToGo() {
      return (int)(MarkerToGo - MarkerCurrent);
    }
  }

  class ActionController : MonoBehaviour {
    public PlayDescription CurrentAction;
    public PlayDescription NextAction;
    [Inject(Id = "Marker1")] public Marker Marker1;
    [Inject(Id = "Marker2")] public Marker Marker2;
    [Inject] Ball Ball;
    public static ActionController Instance;
    void Start() {
      if (Instance != null) {
        Destroy(gameObject);
        return;
      }

      Instance = this;

      if (CurrentAction == null) {
        CurrentAction = new PlayDescription {
          Down = 1,
          MarkerCurrent = 50.0f,
          MarkerToGo = 60.0f
        };
        UpdateMarkers();
      }

      Ball.OnTackle += HandleTackle;

      DontDestroyOnLoad(gameObject);
    }
    public void OnDestroy() {
      Ball.OnTackle -= HandleTackle;
    }
    void HandleTackle() {
      NextAction = GetNextAction();
    }
    public void GoToNextScene() {
      CurrentAction = NextAction;
      NextAction = null;

      StartCoroutine(ReloadScene());
    }
    private IEnumerator ReloadScene() {
      var asyncLoadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
      while (!asyncLoadLevel.isDone) {

        yield return null;
      }
      UpdateRefs();
      UpdateMarkers();
    }
    PlayDescription GetNextAction() {
      return new PlayDescription {
        Down = CurrentAction.Down + 1,
        MarkerCurrent = Converter.XPositionToYards(Ball.transform.position.x),
        MarkerToGo = 60.0f
      };
    }
    void UpdateRefs() {
      Ball = GameObject.Find("Ball").GetComponent<Ball>();
      Marker1 = GameObject.Find("Marker1").GetComponent<Marker>();
      Marker2 = GameObject.Find("Marker2").GetComponent<Marker>();
    }
    void UpdateMarkers() {
      Marker1.ChangeYards(CurrentAction.MarkerCurrent);
      Marker2.ChangeYards(CurrentAction.MarkerToGo);
    }
  }
}
