using System;
using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [Serializable]
    public abstract class WorldObjectAnimatorComponent {
        #region Fields
        [SerializeReference, HideInInspector] protected WorldObjectAnimator animator;
        #endregion

        #region Constructors
        public WorldObjectAnimatorComponent(WorldObjectAnimator animator) {
            this.animator = animator;
        }
        #endregion

        #region Initialization
        public abstract void Initialize();
        public abstract void Disable();
        #endregion
    }
}
