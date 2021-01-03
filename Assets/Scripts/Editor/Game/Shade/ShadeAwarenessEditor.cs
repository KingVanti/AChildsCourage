using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [CustomEditor(typeof(ShadeAwarenessEntity))]
    public class ShadeAwarenessEditor : Editor
    {

        private ShadeAwarenessEntity Awareness => (ShadeAwarenessEntity) target;


        private void OnSceneGUI()
        {
            var angleProgression = Awareness.CurrentAwareness * 360;

            Handles.color = new Color(0.75f, 0.9f, 0.75f, 0.5f);
            Handles.DrawSolidArc(Awareness.transform.position + new Vector3(0, 1.5f, 0), Vector3.forward, Vector3.up, angleProgression, 0.5f);
        }

    }

}