using UnityEngine.SceneManagement;

namespace AChildsCourage
{

    internal static class Transition
    {

        internal static void To(SceneName sceneName) =>
            SceneManager.LoadScene(sceneName);

    }

}