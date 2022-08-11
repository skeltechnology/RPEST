using SkelTech.RPEST.Animations.Sprites.Animators.Components;
using SkelTech.RPEST.Utilities.Structures;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    /// <summary>
    /// <c>MonoBehaviour</c> class that contains a collection of animator components.
    /// </summary>
    public class WorldObjectAnimator : SpriteAnimator {
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
                component.Play();
            }
        }

        protected virtual void OnDestroy() {
            foreach (WorldObjectAnimatorComponent component in components) {
                component.Pause();
            }
        }
        #endregion

        #region Getters
        public List<WorldObjectAnimatorComponent> GetComponents() {
            return this.components;
        }

        public WorldObjectAnimatorComponent FindComponent(Type componentType) {
            if (componentType == null) return null;

            foreach (WorldObjectAnimatorComponent component in this.components) {
                if (component.GetType().Equals(componentType)) {
                    return component;
                } 
            }
            return null;
        }
        #endregion

        #region Setters
        /// <summary>
        /// /// Adds the given component to the collection of animator components and initializes it.
        /// </summary>
        /// <param name="component">Animator component that will be added to the collection.</param>
        public void AddImplementation(WorldObjectAnimatorComponent component) {
            this.components.Add(component);
            component.Play();
        }

        public void RemoveImplementation(Type componentType) {
            WorldObjectAnimatorComponent component = this.FindComponent(componentType);
            if (component != null)
                this.RemoveImplementation(component);
        }

        public void RemoveImplementation(WorldObjectAnimatorComponent component) {
            component.Pause();
            this.components.Remove(component);
        }
        #endregion
    }
}
