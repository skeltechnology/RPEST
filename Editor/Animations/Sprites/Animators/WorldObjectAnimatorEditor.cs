using System;
using System.Linq;

using UnityEngine;
using UnityEditor;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [CustomEditor(typeof(WorldObjectAnimator))]
    class WorldObjectAnimatorEditor : Editor {
        private Type[] implementations;
        private int implementationTypeIndex;

        public override void OnInspectorGUI() {
            WorldObjectAnimator behaviour = this.target as WorldObjectAnimator;

            if (behaviour == null) return;
            
            if (implementations == null || GUILayout.Button("Refresh Implementations")) {
                // Find all implementations of WorldObjectAnimatorComponent using System.Reflection.Module
                implementations = GetImplementations<WorldObjectAnimatorComponent>().Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
            }

            EditorGUILayout.LabelField($"Found {implementations.Length} implementations.");            
            
            // Select implementation from editor popup
            implementationTypeIndex = EditorGUILayout.Popup(new GUIContent("Implementation"),
                implementationTypeIndex, implementations.Select(impl => impl.Name).ToArray());

            if (GUILayout.Button("Create Instance")) {
                object[] args = { behaviour };
                object o = Activator.CreateInstance(implementations[implementationTypeIndex], args);
                WorldObjectAnimatorComponent c = o as WorldObjectAnimatorComponent;
                behaviour.AddComponent(c);
            }

            this.serializedObject.Update();
            SerializedProperty listProp = serializedObject.FindProperty("components");
            DisplayArray(listProp, "Components");        
            serializedObject.ApplyModifiedProperties();
        }
        
        private static Type[] GetImplementations<T>()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            var interfaceType = typeof(T);
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }

        private static void DisplayArray(SerializedProperty array, string caption=null) {
            EditorGUILayout.LabelField((caption == null) ? array.name : caption);
            for (int i = 0; i < array.arraySize; i++) {
                SerializedProperty property = array.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property, new GUIContent(property.managedReferenceFullTypename.Split('.').Last()), true);
            }
        }
    }
}