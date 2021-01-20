using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game
{

    public class StartCutScene : MonoBehaviour
    {

        [UsedImplicitly]
        private void OnStartCutSceneEnded() =>
            Transition.To(SceneName.game, FadeColor.Black);

    }

}