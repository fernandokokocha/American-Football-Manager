using Zenject;
using UnityEngine;

namespace AmericanFootballManager {
  public class SceneInstaller : MonoInstaller {
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
    }
  }
}