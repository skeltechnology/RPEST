using SkelTech.RPEST.World.Elements.Objects;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    /// <summary>
    /// Class for managing <c>Interactable</c>s.
    /// </summary>
    public class InteractableDatabase : WorldDatabase<Interactable> {
        #region Getters
        /// <summary>
        /// Gets the interactable (if any) at the correspondent position.
        /// If there are two or more colliders at that position, only the first one is returned.
        /// </summary>
        /// <param name="globalPosition">Position of the interactable (global coordinates).</param>
        /// <returns>Interactable at the correspondet position. <c>null</c> if there isn't one.</returns>
        public Interactable GetInteractable(Vector3 globalPosition) {
            return GetFirst(this.database, (interactable) => {
                return interactable.GetWorldObject().Intersects(globalPosition); 
            });
        }

        /// <summary>
        /// Gets all the interactables (if any) at the correspondent position.
        /// </summary>
        /// <param name="globalPosition">Position of the interactables (global coordinates).</param>
        /// <returns>Collection of interactables at the correspondet position.</returns>
        public ICollection<Interactable> GetInteractables(Vector3 globalPosition) {
            return GetAll(this.database, (interactable) => {
                return interactable.GetWorldObject().Intersects(globalPosition); 
            });
        }
        #endregion
    }
}
