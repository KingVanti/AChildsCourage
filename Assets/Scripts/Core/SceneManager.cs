using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.ServiceInjecting;

namespace AChildsCourage
{

    public class SceneManager : MonoBehaviour
    {

        public Events.Empty onSceneLoaded;


        private void Awake() => SetupScene();

        private void SetupScene()
        {
            InjectServices();
            onSceneLoaded.Invoke();
        }


        protected static void LoadSceneWith(string sceneName) => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

    }

}