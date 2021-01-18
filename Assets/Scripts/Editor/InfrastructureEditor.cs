using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static AChildsCourage.Infrastructure;

namespace AChildsCourage
{

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class InfrastructureEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var messages = GetInfrastructureMessages(target.GetType()).ToArray();

            if (messages.Any()) DrawInfrastructureMessages(messages);
        }

        private static IEnumerable<string> GetInfrastructureMessages(Type targetType) =>
            GetFieldMessages(targetType.GetFields(DefaultBindingFlags));

        private static IEnumerable<string> GetFieldMessages(FieldInfo[] fields) =>
            GetFindComponentMessages(fields).Concat(GetFindInSceneMessages(fields));

        private static IEnumerable<string> GetFindInSceneMessages(IEnumerable<FieldInfo> fields) =>
            fields.Where(HasAttribute<FindInSceneAttribute>).Select(field => $"{field.FieldType.Name} \"{field.Name}\" is found in the scene");

        private static IEnumerable<string> GetFindComponentMessages(IEnumerable<FieldInfo> fields) =>
            fields
                .Where(HasAttribute<FindComponentAttribute>).Select(field =>
                {
                    var findMode = field.GetCustomAttribute<FindComponentAttribute>().FindMode;
                    var findModeString =
                        findMode == ComponentFindMode.OnSelf ? "this game-object"
                        : findMode == ComponentFindMode.OnChildren ? "one of the game-objects children"
                        : "the game-objects parent";

                    return $"{field.FieldType.Name} \"{field.Name}\" is found on {findModeString}";
                });

        private static void DrawInfrastructureMessages(IEnumerable<string> messages)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            foreach (var message in messages) EditorGUILayout.LabelField(message);
            EditorGUILayout.EndVertical();
        }

    }

}