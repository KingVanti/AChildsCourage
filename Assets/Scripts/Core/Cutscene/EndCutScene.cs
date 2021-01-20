using UnityEngine;

namespace AChildsCourage.Game
{

    public class EndCutScene : MonoBehaviour
    {

        private void OnEndCutSceneEnded() =>
            Transition.To(SceneName.menu, FadeColor.Black);

    }

}