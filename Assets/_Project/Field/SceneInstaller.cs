using Zenject;
using UnityEngine;

namespace AmericanFootballManager {
    public class SceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
               .Bind<Team>()
               .FromComponentInParents();
        }
    }
}