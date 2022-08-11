using SkelTech.RPEST.Utilities.Structures;

using System;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    /// <summary>
    /// Custom editor for the <c>WorldObjectAnimator</c> class.
    /// </summary>
    [CustomEditor(typeof(WorldObjectAnimator))]
    public class WorldObjectAnimatorEditor : SelectImplementationEditor<WorldObjectAnimator, WorldObjectAnimatorComponent> {
        #region Unity
        protected void Awake() {
            this.Initialize("components", "Components", true);
        }

        protected override void OnEnable() {
            base.OnEnable();
            this.reorderableList.onRemoveCallback = (ReorderableList list) => {
                int index = list.index;
                if (index < 0 || index > list.count)
                    return;
                
                SerializedProperty serializedProperty = list.serializedProperty;
                
                WorldObjectAnimatorComponent component = (WorldObjectAnimatorComponent) serializedProperty.GetArrayElementAtIndex(index).managedReferenceValue;
                if (component != null)
                    component.Pause();

                serializedProperty.DeleteArrayElementAtIndex(index);
                list.index = -1;
            };
        }
        #endregion

        #region Initialization
        protected override void AddEditorImplementation(WorldObjectAnimatorComponent component) {
            this.behaviour.GetComponents().Add(component);
        }
        #endregion

        #region Helpers
        protected override WorldObjectAnimatorComponent CreateImplementation(Type type) {
            object[] args = { this.behaviour };
            object o = Activator.CreateInstance(type, args);
            return (WorldObjectAnimatorComponent) o;
        }
        #endregion
    }
}