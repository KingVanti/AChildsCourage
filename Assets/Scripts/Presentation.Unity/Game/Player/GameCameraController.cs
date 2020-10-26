using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class GameCameraController : MonoBehaviour {

        [SerializeField] private CharacterController characterController;

        private void LateUpdate() {

            transform.position = characterController.transform.position + new Vector3(0, 0, -10);

        }



    }

}
