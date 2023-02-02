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
            foreach (GameObject player in players) {
                if (player.GetComponent<PlayerPosition>().Position == Position.QB) {
                    Container
                      .Bind<PlayerPosition>()
                      .FromInstance(player.GetComponent<PlayerPosition>());

                    break;
                }
            }

            Container
              .Bind<Interface>()
              .FromComponentInHierarchy()
              .AsSingle();
        }
    }
}