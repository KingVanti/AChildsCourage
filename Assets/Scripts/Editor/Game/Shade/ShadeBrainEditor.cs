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
            if (Brain.CurrentMoveTarget.HasValue) Handles.DrawSolidDisc(Brain.CurrentMoveTarget.Value, Vector3.forward, 0.25f);
        }

    }

}