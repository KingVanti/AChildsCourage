using UnityEditor;
using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game.Shade
{

    [CustomEditor(typeof(ShadeEyesEntity))]
    public class ShadeEyesEditor : Editor
    {

        private static readonly Color tileInVisionColor = new Color(0.31f, 0.37f, 0.9f, 1f);


        private ShadeEyesEntity Eyes => target as ShadeEyesEntity;


        private void OnSceneGUI()
        {
            var shadePosition = Eyes.transform.position;

            foreach (var visionCone in Eyes.VisionCones) DrawVisionCone(shadePosition, visionCone);
        }

        private void DrawVisionCone(Vector3 shadePosition, VisionCone visionCone)
        {
            Handles.color = visionCone.Visibility.Equals(Visibility.Primary)
                ? new Color(1f, 0.22f, 0.25f, 0.2f)
                : new Color(1f, 0.69f, 0.24f, 0.2f);

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, visionCone.ViewRadius);


            Handles.DrawSolidArc(shadePosition, Vector3.forward, ToVector(Eyes.transform.eulerAngles.z - visionCone.ViewAngle / 2f), visionCone.ViewAngle, visionCone.ViewRadius);
        }

        private static Vector3 ToVector(float angle) =>
            new Vector3(Cos(angle * Deg2Rad),
                        Sin(angle * Deg2Rad));

    }

}