using System;
using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [Serializable]
    /// <summary>
    /// Base class for animator components.
    /// </summary>
    public abstract class WorldObjectAnimatorComponent {
        #region Fields
        /// <summary>
        /// Reference to the animator that manages this component.
        /// </summary>
        [SerializeReference, HideInInspector] protected WorldObjectAnimator animator;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public WorldObjectAnimatorComponent(WorldObjectAnimator animator) {
            this.animator = animator;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Method responsible for initializing the component.
        /// Every listening operation should be performed here.
        /// This method is called as soon as the animator is created.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Method responsible for disabling the necessary operations.
        /// Every unlistening operation should be performed here.
        /// This mehod is called as soon as the animator is destroyed.
        /// </summary>
        public abstract void Disable();
        #endregion
    }
}
