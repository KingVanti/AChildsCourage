using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Menu {
    public class CompletionCheck : MonoBehaviour {

        [SerializeField] private Image completionCheckImage;

        private bool HasCompletedGame {
            get => PlayerPrefs.GetInt("COMPLETED") == 1;
        }

        private void Start() => completionCheckImage.enabled = HasCompletedGame;

    }

}
