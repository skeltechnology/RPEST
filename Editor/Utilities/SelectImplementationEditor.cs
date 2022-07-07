using SkelTech.RPEST.Utilities.Structures;

using System;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace SkelTech.RPEST.Utilities {
    public abstract class SelectImplementationEditor<A, B> : Editor where A : UnityEngine.Object, SelectImplementation<B> {
        #region Fields
        private Type[] implementations;
        private int implementationTypeIndex;

        protected A behaviour;
        private SerializedProperty listProperty;
        private ReorderableList reorderableList; 

        private string listName;
        private string caption;
        private bool uniqueImplementations = false;
        #endregion

        #region Unity
        protected void Awake(string listName, string caption, bool uniqueImplementations) {
            this.listName = listName;
            this.caption = caption;
            this.uniqueImplementations = uniqueImplementations;
        }

        private void OnEnable() {
            this.behaviour = this.target as A;
            this.listProperty = serializedObject.FindProperty(this.listName);
            this.reorderableList = new ReorderableList(this.serializedObject, this.listProperty, true, true, false, true);
            this.reorderableList.drawHeaderCallback = rect => {
                EditorGUI.LabelField(rect, this.caption, EditorStyles.boldLabel);
            };
            this.reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                if (this.listProperty.isExpanded) {
                    EditorGUI.indentLevel = 1;
                    SerializedProperty property = listProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, property, new GUIContent(property.managedReferenceFullTypename.Split('.').Last()), true); // TODO: NAME CHOICE
                }
            };
            this.reorderableList.elementHeightCallback = (int index) => {
                SerializedProperty property = listProperty.GetArrayElementAtIndex(index);
                return property.isExpanded ? this.reorderableList.elementHeight * property.CountInProperty()  : EditorGUIUtility.singleLineHeight;
            };
        }

        public override void OnInspectorGUI() {
            this.serializedObject.Update();
            this.DrawInterface();
            this.reorderableList.DoLayoutList();        
            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region Helpers
        protected abstract B CreateImplementation(Type type);

        private void DrawInterface() {
            if (behaviour == null) return;
            
            if (implementations == null || GUILayout.Button("Refresh Implementations")) {
                // Find all implementations of WorldObjectAnimatorComponent using System.Reflection.Module
                implementations = GetImplementations().Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
            }

            EditorGUILayout.LabelField($"Found {implementations.Length} implementations.");            
            
            // Select implementation from editor popup
            implementationTypeIndex = EditorGUILayout.Popup(new GUIContent("Implementation"),
                implementationTypeIndex, implementations.Select(impl => impl.Name).ToArray());

            if (GUILayout.Button("Create Instance")) {
                if (this.uniqueImplementations && this.Contains(implementations[implementationTypeIndex].FullName)) {
                    Debug.LogWarning("Invalid Operation. This list must contain a unique implementation for each class.");
                } else {
                    behaviour.AddImplementation(this.CreateImplementation(implementations[implementationTypeIndex]));
                }
            }
        }

        private static Type[] GetImplementations() {
            System.Collections.Generic.IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
            Type interfaceType = typeof(B);
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }

        private bool Contains(string typeFullname) {
            for (int i = 0; i < this.listProperty.arraySize; ++i) {
                if (this.listProperty.GetArrayElementAtIndex(i).managedReferenceFullTypename.Split(' ').Last().Equals(typeFullname))
                    return true;
            }
            return false;
        }
        #endregion
    }
}