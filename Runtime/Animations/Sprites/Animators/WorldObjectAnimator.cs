using SkelTech.RPEST.Animations.Sprites.Animators.Components;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    public class WorldObjectAnimator : SpriteAnimator {
        #region Fields
        [SerializeReference, NonReorderable] private List<WorldObjectAnimatorComponent> components;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            foreach (WorldObjectAnimatorComponent component in components) {
                component.Initialize();
            }
        }

        protected void OnDestroy() {
            foreach (WorldObjectAnimatorComponent component in components) {
                component.Disable();
            }
        }
        #endregion

        #region Setters
        public void AddComponent(WorldObjectAnimatorComponent component) {
            this.components.Add(component);
        }
        #endregion
    }
}
