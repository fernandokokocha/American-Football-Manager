using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using System.Collections;

namespace AmericanFootballManager {
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

      CurrentAction.SetFirstActionData();
      UpdateMarkers();

      Ball.OnTackle += HandleTackle;

      DontDestroyOnLoad(gameObject);
    }
    public void OnDestroy() {
      Ball.OnTackle -= HandleTackle;
      CurrentAction.SetFirstActionData();
    }
    void HandleTackle() {
      NextAction = GetNextAction();
    }
    public void GoToNextScene() {
      CurrentAction.CopyFrom(NextAction);
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
      PlayDescription NewAction = ScriptableObject.CreateInstance<PlayDescription>();
      NewAction.Down = CurrentAction.Down + 1;
      NewAction.MarkerCurrent = Converter.XPositionToYards(Ball.transform.position.x);
      NewAction.MarkerToGo = 60.0f;
      return NewAction;
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
