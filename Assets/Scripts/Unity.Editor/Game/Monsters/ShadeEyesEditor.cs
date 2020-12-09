using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Monsters
{

    [CustomEditor(typeof(ShadeEyes))]
    public class ShadeEyesEditor : Editor
    {

        private ShadeEyes Eyes => target as ShadeEyes;


        private void OnSceneGUI()
        {
            var shadePosition = Eyes.transform.position;

            Handles.color = Eyes.CharacterIsInVision ? Color.red : Color.white;

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, Eyes.ViewRadius);

            Handles.color = new Color(1, 1, 1, 0.25f);

            Handles.DrawSolidArc(shadePosition, Vector3.forward, (Eyes.transform.eulerAngles.z - (Eyes.ViewAngle / 2f)).ToVector(), Eyes.ViewAngle, Eyes.ViewRadius);
        }

    }

}