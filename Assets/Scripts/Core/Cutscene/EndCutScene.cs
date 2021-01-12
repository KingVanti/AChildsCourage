using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage.Game
{
    public class EndCutScene : MonoBehaviour
    {
        private void OnEndCutSceneEnded() =>
          Transition.To(SceneName.end);
    }
}
