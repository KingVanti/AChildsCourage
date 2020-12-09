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

            DrawPrimaryVision(shadePosition, Eyes.PrimaryVision);
            DrawSecondaryVision(shadePosition, Eyes.SecondaryVision);
        }

        private void DrawPrimaryVision(Vector3 shadePosition, VisionCone visionCone)
        {
            Handles.color = Eyes.CharacterVisibility == Visibility.Primary ? Color.red : Color.white;

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, visionCone.ViewRadius);

            Handles.color = new Color(1, 1, 1, 0.25f);

            Handles.DrawSolidArc(shadePosition, Vector3.forward, (Eyes.transform.eulerAngles.z - visionCone.ViewAngle / 2f).ToVector(), visionCone.ViewAngle, visionCone.ViewRadius);
        }
        
        private void DrawSecondaryVision(Vector3 shadePosition, VisionCone visionCone)
        {
            Handles.color = Eyes.CharacterVisibility == Visibility.Secondary ? Color.red : Color.white;

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, visionCone.ViewRadius);

            Handles.color = new Color(1, 1, 1, 0.125f);

            Handles.DrawSolidArc(shadePosition, Vector3.forward, (Eyes.transform.eulerAngles.z - visionCone.ViewAngle / 2f).ToVector(), visionCone.ViewAngle, visionCone.ViewRadius);
        }

    }

}