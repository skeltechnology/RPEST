using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public abstract class InteractableObject : WorldObject, Interactable {
        #region Getters
        public WorldObject GetWorldObject() {
            return this;
        }
        #endregion

        #region Operators
        public abstract bool Interact(InteractorObject interactor);
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.world.InteractableDatabase.Add(this);
            base.InitializeWorldElement();
        }

        protected override void DisableWorldElement() {
            this.world.InteractableDatabase.Remove(this);
            base.DisableWorldElement();
        }
        #endregion
    }
}
