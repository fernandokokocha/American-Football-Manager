using Zenject;
using UnityEngine;
// using AmericanFootballManager;

namespace AmericanFootballManager {
    public class SceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
               .Bind<Team>()
               .FromComponentInParents();
        }
    }
}