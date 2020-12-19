using UnityEditor;
using UnityEngine;
using static AChildsCourage.MRange;
using static UnityEditor.EditorGUI;

namespace AChildsCourage
{

    [CustomPropertyDrawer(typeof(Range<>))]
    public class RangeEditor : PropertyDrawer
    {

        private const float ValueLabelWidth = 30;
        private const float ValueSpacing = 10;

        private static readonly float lineHeight = EditorGUIUtility.singleLineHeight;
        private static readonly float spacing = EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect PositionWith(float xOffset, float width) => new Rect(position.x + xOffset, position.y, width, position.height);

            LabelField(PositionWith(0, EditorGUIUtility.labelWidth), label);

            var propertyWidth = (position.width - EditorGUIUtility.labelWidth - ValueSpacing) / 2f;
            var valueWidth = propertyWidth - ValueLabelWidth;

            LabelField(PositionWith(EditorGUIUtility.labelWidth, ValueLabelWidth), "Min");
            PropertyField(PositionWith(EditorGUIUtility.labelWidth + ValueLabelWidth, valueWidth), property.FindPropertyRelative("min"), GUIContent.none);

            LabelField(PositionWith(EditorGUIUtility.labelWidth + propertyWidth + ValueSpacing, ValueLabelWidth), "Max");
            PropertyField(PositionWith(EditorGUIUtility.labelWidth + propertyWidth + ValueLabelWidth + ValueSpacing, valueWidth), property.FindPropertyRelative("max"), GUIContent.none);
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => lineHeight + spacing;

    }

}