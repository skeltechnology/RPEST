using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.World.Objects {
    public abstract class InteractableObject : WorldObject, Interactable {
        #region Unity
        // Inform world of interactable
        #endregion

        #region Getters
        public WorldObject GetWorldObject() {
            return this;
        }
        #endregion

        #region Operators
        public abstract bool Interact(InteractorObject interactor);
        #endregion
    }
}
