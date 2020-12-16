using UnityEditor;
using UnityEngine;
using static UnityEngine.Mathf;
using static AChildsCourage.Game.MTilePosition;

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

            DrawPrimaryVision(shadePosition, Eyes.PrimaryVision);
            DrawSecondaryVision(shadePosition, Eyes.SecondaryVision);
            DrawTilesInView();
        }

        private void DrawPrimaryVision(Vector3 shadePosition, VisionCone visionCone)
        {
            Handles.color = Eyes.CharacterVisibility == Visibility.Primary
                ? Color.red
                : Color.white;

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, visionCone.ViewRadius);

            Handles.color = new Color(1, 1, 1, 0.25f);

            Handles.DrawSolidArc(shadePosition, Vector3.forward, ToVector(Eyes.transform.eulerAngles.z - visionCone.ViewAngle / 2f), visionCone.ViewAngle, visionCone.ViewRadius);
        }

        private void DrawSecondaryVision(Vector3 shadePosition, VisionCone visionCone)
        {
            Handles.color = Eyes.CharacterVisibility == Visibility.Secondary
                ? Color.red
                : Color.white;

            Handles.DrawWireArc(shadePosition, Vector3.forward, Vector3.right, 360, visionCone.ViewRadius);

            Handles.color = new Color(1, 1, 1, 0.125f);

            Handles.DrawSolidArc(shadePosition, Vector3.forward, ToVector(Eyes.transform.eulerAngles.z - visionCone.ViewAngle / 2f), visionCone.ViewAngle, visionCone.ViewRadius);
        }

        private void DrawTilesInView()
        {
            foreach (var tilePosition in Eyes.CurrentTilesInView)
            {
                var rect = new Rect(tilePosition.Map(ToVector2), Vector2.one);

                Handles.DrawSolidRectangleWithOutline(rect, tileInVisionColor, tileInVisionColor);
            }
        }

        private Vector3 ToVector(float angle) =>
            new Vector3(Cos(angle * Deg2Rad),
                        Sin(angle * Deg2Rad));

    }

}