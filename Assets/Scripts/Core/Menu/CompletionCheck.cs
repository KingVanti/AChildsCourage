using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Menu
{

    public class CompletionCheck : MonoBehaviour
    {

        private const string CompletionPlayerPrefKey = "COMPLETED";


        [SerializeField] private Image completionCheckImage;

        private static bool HasCompletedGame => PlayerPrefs.GetInt(CompletionPlayerPrefKey) == 1;

        private void Start() =>
            completionCheckImage.enabled = HasCompletedGame;

    }

}