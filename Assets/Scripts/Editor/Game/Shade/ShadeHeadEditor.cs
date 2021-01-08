using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [CustomEditor(typeof(ShadeHeadEntity))]
    public class ShadeHeadEditor : Editor
    {

        private ShadeHeadEntity Head => (ShadeHeadEntity) target;


        private void OnSceneGUI()
        {
            if (Head.ExplicitTargetPosition.HasValue)
            {
                Handles.color = new Color(0.57f, 0.54f, 0.16f, 0.62f);
                Handles.DrawSolidDisc(Head.ExplicitTargetPosition.Value, Vector3.forward, 0.25f);
            }
        }

    }

}