using SkelTech.RPEST.Animations.Sprites.Animators.Components;
using SkelTech.RPEST.Utilities.Structures;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    /// <summary>
    /// <c>MonoBehaviour</c> class that contains a collection of animator components.
    /// </summary>
    public class WorldObjectAnimator : SpriteAnimator, SelectImplementation<WorldObjectAnimatorComponent> {
        #region Fields
        /// <summary>
        /// Collection of animator components.
        /// </summary>
        [SerializeReference] private List<WorldObjectAnimatorComponent> components;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            foreach (WorldObjectAnimatorComponent component in components) {
                component.Initialize();
            }
        }

        protected virtual void OnDestroy() {
            foreach (WorldObjectAnimatorComponent component in components) {
                component.Disable();
            }
        }
        #endregion

        #region Setters
        /// <summary>
        /// Adds the given component to the collection of animator components.
        /// </summary>
        /// <param name="component">Animator component that will be added to the collection.</param>
        public void AddImplementation(WorldObjectAnimatorComponent component) {
            this.components.Add(component);
        }
        #endregion
    }
}
