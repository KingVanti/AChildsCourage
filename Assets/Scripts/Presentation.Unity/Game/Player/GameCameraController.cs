using UnityEngine;

namespace AChildsCourage.Game.Player
{
    public class GameCameraController : MonoBehaviour
    {


        public void UpdatePosition(Vector2 newPosition)
        {

            transform.position = new Vector3(newPosition.x, newPosition.y, -10.0f);


        }

    }

}
