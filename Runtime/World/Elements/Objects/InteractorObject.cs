using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class InteractorObject : WalkableObject {
        #region Fields
        [SerializeField] private string interactorId;
        #endregion

        #region Operators
        public bool Interact() {
            // Check if there's an interactable in the next cell
            // If there is, interact with it
            return false;
        }
        #endregion
    }
}
