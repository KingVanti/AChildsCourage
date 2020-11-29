using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.ServiceInjecting;

namespace AChildsCourage
{

    public class SceneManager : MonoBehaviour
    {

        #region Fields

        public UnityEvent onSceneLoaded;

        #endregion

        #region Methods

        private void Awake()
        {
            SetupScene();
        }

        private void SetupScene()
        {
            InjectServices();
            onSceneLoaded.Invoke();
        }

        public void LoadSceneWith(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }

        #endregion

    }

}