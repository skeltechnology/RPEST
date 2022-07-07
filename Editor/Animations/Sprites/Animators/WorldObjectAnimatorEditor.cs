using SkelTech.RPEST.Utilities;

using UnityEditor;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [CustomEditor(typeof(WorldObjectAnimator))]
    public class WorldObjectAnimatorEditor : SelectImplementationEditor<WorldObjectAnimator, WorldObjectAnimatorComponent> {
        #region Unity
        protected void Awake() {
            this.Awake("components", "Components", true);
        }
        #endregion
    }
}