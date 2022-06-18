using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public abstract class InteractableObject : WorldObject, Interactable {
        #region Unity
        // Inform world of interactable
        #endregion

        #region Getters
        public WorldObject GetWorldObject() {
            return this;
        }
        #endregion

        #region Setters
        public override void SetWorld(World world) {
            this.world?.InteractableDatabase.Remove(this);
            base.SetWorld(world);
            this.world.InteractableDatabase.Add(this);
        }
        #endregion

        #region Operators
        public abstract bool Interact(InteractorObject interactor);
        #endregion
    }
}
