using UnityEngine;
using static AChildsCourage.Infrastructure.MInfrastructure;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        public Events.Empty onSceneLoaded;


        private void Awake() => SetupScene();

        private void SetupScene()
        {
            SetupSceneInfrastructure();
            onSceneLoaded.Invoke();
        }

    }

}