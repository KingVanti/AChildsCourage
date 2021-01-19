using UnityEngine;

namespace AChildsCourage.Game
{

    public class StartCutScene : MonoBehaviour
    {

        private void OnStartCutSceneEnded() =>
            Transition.To(SceneName.game, FadeColor.Black);

    }

}