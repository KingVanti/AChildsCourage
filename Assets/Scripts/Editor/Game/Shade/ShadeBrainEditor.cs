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
            Handles.color = new Color(0.74f, 0.17f, 0.13f, 0.65f);
            if (Brain.MoveTarget.HasValue) Handles.DrawSolidDisc(Brain.MoveTarget.Value, Vector3.forward, 0.25f);
            Handles.color = new Color(0.74f, 0.68f, 0.15f, 0.65f);
            if (Brain.LookTarget.HasValue) Handles.DrawSolidDisc(Brain.LookTarget.Value, Vector3.forward, 0.25f);
        }

    }

}