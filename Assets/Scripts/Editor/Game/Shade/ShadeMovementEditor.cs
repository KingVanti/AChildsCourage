using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [CustomEditor(typeof(ShadeMovementEntity))]
    public class ShadeMovementEditor : Editor
    {

        private ShadeMovementEntity Movement => (ShadeMovementEntity) target;


        private void OnSceneGUI()
        {
            Handles.color = new Color(0.57f, 0.11f, 0.14f, 0.62f);
            Handles.DrawSolidDisc(Movement.AiTarget, Vector3.forward, 0.25f);
        }

    }

}