using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class InteractorObject : WalkableObject {
        #region Fields
        [SerializeField] private string interactorId;
        #endregion

        #region Operators
        public void Interact() {
            Vector3 interactablePosition = this.transform.position + this.lastDirection;
            Interactable interactable = this.world.InteractableDatabase.GetInteractable(interactablePosition);

            // Check if there's an interactable in the next cell
            if (interactable != null) {
                interactable.Interact(this);
            }
        }
        #endregion
    }
}
