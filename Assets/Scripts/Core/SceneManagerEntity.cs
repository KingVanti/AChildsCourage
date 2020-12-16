using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.ServiceInjecting;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        public Events.Empty onSceneLoaded;


        private void Awake() => SetupScene();

        private void SetupScene()
        {
            InjectServices();
            onSceneLoaded.Invoke();
        }
        
    }

}