using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage
{

    [CustomPropertyDrawer(typeof(EnumArray<,>))]
    public class EnumArrayEditor : PropertyDrawer
    {

        private const string ArrayPropertyName = "enums";

        private static readonly float xOffset = EditorGUIUtility.singleLineHeight;
        private static readonly float lineHeight = EditorGUIUtility.singleLineHeight;
        private static readonly float spacing = EditorGUIUtility.standardVerticalSpacing;

        private bool showArray;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = new Rect(xOffset, position.y, position.width, lineHeight);

            showArray = EditorGUI.Foldout(position, showArray, label);
            position.y += lineHeight + spacing;

            if (showArray) DrawArray(position, property);
        }

        private static Rect Indent(Rect position)
        {
            position.x += xOffset;
            position.width -= xOffset;

            return position;
        }

        private static void DrawArray(Rect position, SerializedProperty property)
        {
            position = Indent(position);

            var array = GetArrayProperty(property);
            var enumType = GetEnumType(property);

            var enumValues = Enum.GetValues(enumType);

            foreach (var mappedEnum in GetArrayElements(array))
            {
                var enumName = enumValues.GetValue(mappedEnum.FindPropertyRelative("enumValue").enumValueIndex).ToString();
                var mappedValueProperty = mappedEnum.FindPropertyRelative("mappedValue");
                EditorGUI.PropertyField(position, mappedValueProperty, new GUIContent(enumName));

                position.y += lineHeight;
            }
        }

        private static IEnumerable<SerializedProperty> GetArrayElements(SerializedProperty array)
        {
            foreach (var element in array) yield return (SerializedProperty) element;
        }

        private static Type GetEnumType(SerializedProperty property) =>
            property.FindPropertyRelative("enumTypeName")
                    .stringValue
                    .Map(Type.GetType);


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => (showArray ? lineHeight * GetArrayLength(property) : 0) + lineHeight + spacing;

        private static int GetArrayLength(SerializedProperty property) => GetArrayProperty(property).arraySize;


        private static SerializedProperty GetArrayProperty(SerializedProperty property) => property.FindPropertyRelative(ArrayPropertyName);

    }

}