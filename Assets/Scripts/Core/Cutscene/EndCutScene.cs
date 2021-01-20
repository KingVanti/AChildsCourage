using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game
{

    public class EndCutScene : MonoBehaviour
    {

        [UsedImplicitly]
        private void OnEndCutSceneEnded() =>
            Transition.To(SceneName.menu, FadeColor.Black);

    }

}