using UnityEngine;
using UnityEditor;

namespace SkelTech.RPEST.Utilities.Structures {
    [CustomPropertyDrawer(typeof(Direction))]
    public class DirectionPropertyDrawer : PropertyDrawer {
        #region Unity
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var rect = new Rect(position.x, position.y, position.width, position.height);

            EditorGUI.PropertyField(rect, property.FindPropertyRelative("direction"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
        #endregion
    }
}
