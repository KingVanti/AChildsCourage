using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [CustomEditor(typeof(ShadeBrainEntity))]
    public class ShadeBrainEditor : Editor
    {

        private ShadeBrainEntity Brain => (ShadeBrainEntity) target;


        private void OnSceneGUI()
        {
            Handles.color = new Color(0.63f, 0.16f, 0.12f);
            Handles.DrawSolidDisc(Brain.CurrentTargetPosition, Vector3.forward, 0.5f);
        }

    }

}