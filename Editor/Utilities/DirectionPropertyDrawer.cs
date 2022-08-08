using System;

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

            Rect rect = new Rect(position.x, position.y, position.width, position.height);

            SerializedProperty enumProperty = property.FindPropertyRelative("direction");
            Enum option = EditorGUI.EnumPopup(rect, (Direction.DirectionEnum) enumProperty.enumValueIndex);

            Direction direction = Direction.FromInt(Convert.ToInt32(option));
            property.managedReferenceValue = direction;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
        #endregion
    }
}
