using Zenject;
using UnityEngine;

namespace AmericanFootballManager {
  public class SceneInstaller : MonoInstaller {
    public GameObject markerPrefab;
    public GameObject formationWindowPrefab;
    public override void InstallBindings() {
      Container
        .Bind<Team>()
        .FromComponentInParents();

      Container
        .Bind<Ball>()
        .FromComponentInHierarchy()
        .AsSingle();

      GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
      Container
        .Bind<GameObject[]>()
        .WithId("Players")
        .FromInstance(players);

      foreach (GameObject player in players) {
        if (player.GetComponent<PlayerPosition>().Position == Position.QB) {
          Container
            .Bind<PlayerPosition>()
            .WithId("QB")
            .FromInstance(player.GetComponent<PlayerPosition>());
        }

        if (player.GetComponent<PlayerPosition>().Position == Position.C) {
          Container
            .Bind<PlayerAppearence>()
            .WithId("C")
            .FromInstance(player.GetComponent<PlayerAppearence>());
        }
      }

      Container
        .Bind<Interface>()
        .FromComponentInHierarchy()
        .AsSingle();

      Container
        .Bind<Indicator>()
        .FromComponentInHierarchy()
        .AsSingle();

      Container
        .Bind<Marker>()
        .WithId("Marker1")
        .FromComponentInNewPrefab(markerPrefab)
        .AsTransient();

      Container
        .Bind<Marker>()
        .WithId("Marker2")
        .FromComponentInNewPrefab(markerPrefab)
        .AsTransient();

      Container
        .Bind<ActionController>()
        .FromComponentInHierarchy()
        .AsSingle();

      Container
        .Bind<FormationWindow>()
        .FromComponentInNewPrefab(formationWindowPrefab)
        .AsSingle();
    }
  }
}