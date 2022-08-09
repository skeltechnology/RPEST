using SkelTech.RPEST.Utilities.Structures;

using System;

using UnityEditor;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    /// <summary>
    /// Custom editor for the <c>WorldObjectAnimator</c> class.
    /// </summary>
    [CustomEditor(typeof(WorldObjectAnimator))]
    public class WorldObjectAnimatorEditor : SelectImplementationEditor<WorldObjectAnimator, WorldObjectAnimatorComponent> {
        #region Unity
        protected void Awake() {
            this.Awake("components", "Components", true);
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